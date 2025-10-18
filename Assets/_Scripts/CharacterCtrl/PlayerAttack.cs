using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class PlayerAttack : MMono, IData<PlayerCore>
{

    [SerializeField] private PlayerCore coreFighter;
    [SerializeField] private PlayerBuffs buffSys;
    [SerializeField] private List<PlayerWeaponSlot> weaponSlots = new List<PlayerWeaponSlot>();
    [SerializeField] private PlayerWeaponSlot currentWeaponSlot;
    [SerializeField] private Transform meleeAttackPoint;
    [SerializeField] private LayerMask enemyLayers;
    [SerializeField] private float meleeAttackRange = 2f;
    [SerializeField] private SlashFx slashEffectPrefab;
    [SerializeField] private RangedAttackRange rangedAttackRange;

    private float lastShootTime = -Mathf.Infinity;

    const float RANGED_ATTACK_RANGE = 7.06f;

    private void Update()
    {
        HandleInput();
        UpdateAttackPointPosition();

    }

    private void HandleInput()
    {
        InputManager input = InputManager.Instance;
        if (input.GetMeleeAttackInput()) DoMeleeAttack();
        else if (input.GetAttackDirVector() != Vector2.zero)
        {
            Debug.Log("shoot");
            DoRangedAttack();
        }

        if (input.SwitchToWeaponSlotOne()) SwitchToWeaponSlot(0);
        else if (input.SwitchToWeaponSlotTwo()) SwitchToWeaponSlot(1);
    }

    private void SwitchToWeaponSlot(int slotIndex)
    {

        if (slotIndex < 0 || slotIndex >= weaponSlots.Count || currentWeaponSlot == weaponSlots[slotIndex]) return;
        if (currentWeaponSlot != null) currentWeaponSlot.HideWeapon();
        currentWeaponSlot = weaponSlots[slotIndex];
        currentWeaponSlot.ShowWeapon();
    }

    private void UpdateAttackPointPosition()
    {
        if (InputManager.Instance.GetMovementVector() != Vector2.zero)
        {
            meleeAttackPoint.localPosition = InputManager.Instance.GetMovementVector() * 0.5f + Vector2.down * 0.25f;
            if (!InputManager.Instance.GetRangedAttackInput()) rangedAttackRange.transform.localPosition = InputManager.Instance.GetMovementVector() * 0 * RANGED_ATTACK_RANGE;
        }
    }


    private void DoMeleeAttack()
    {

        if (currentWeaponSlot == null || currentWeaponSlot.weaponProfile == null) return;
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(meleeAttackPoint.position, meleeAttackRange, enemyLayers);
        SlashFx slashEffect = Instantiate(slashEffectPrefab, meleeAttackPoint.position, meleeAttackPoint.rotation);
        slashEffect.gameObject.SetActive(true);
        slashEffect.SetTransform(meleeAttackPoint.transform);
        foreach (Collider2D enemy in hitEnemies)
        {
            EnemyDamageReceiver enemyDamageReceiver = enemy.GetComponent<EnemyDamageReceiver>();
            if (enemyDamageReceiver != null)
            {
                enemyDamageReceiver.TakeDamage(currentWeaponSlot.weaponProfile.meleeDamage + (int)buffSys.GetBonus(BuffType.Attack));
                enemyDamageReceiver.GetKnockback((enemy.transform.position - meleeAttackPoint.position).normalized);
                Debug.Log("Enemy got " + currentWeaponSlot.weaponProfile.meleeDamage + " damage!");
            }
        }

    }


    private void DoRangedAttack()
    {
        if (!CanShoot()) return;
        BulletMoving newBullet = Instantiate(currentWeaponSlot.weaponProfile.bulletPrefab, currentWeaponSlot.transform.position, currentWeaponSlot.transform.rotation);
        newBullet.SetRotation(InputManager.Instance.GetAttackDirVector());
        currentWeaponSlot.BulletDecrease();
        lastShootTime = Time.time;
    }
    private bool CanShoot()
    {
        if (currentWeaponSlot == null || currentWeaponSlot.weaponProfile == null) return false;
        WeaponProfile currentWeapon = currentWeaponSlot.weaponProfile;
        return lastShootTime + currentWeapon.attackSpeed <= Time.time;

    }

    protected override void LoadComponents()
    {

        if (enemyLayers == 0) enemyLayers = LayerMask.GetMask("EnemyDamageReceiver");
        if (weaponSlots.Count == 0) weaponSlots = new List<PlayerWeaponSlot>(GetComponentsInChildren<PlayerWeaponSlot>());

    }

    public void Init(PlayerCore core)
    {
        coreFighter = core;
        if (buffSys == null) buffSys = coreFighter.BuffMng;
        if (meleeAttackPoint == null) meleeAttackPoint = coreFighter.MeleeAttackPoint;
        if (slashEffectPrefab == null) slashEffectPrefab = meleeAttackPoint.Find("SlashEffect").GetComponent<SlashFx>();
    }
}
