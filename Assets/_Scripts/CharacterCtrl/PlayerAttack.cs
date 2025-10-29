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
    [SerializeField] private RangedAttackRange aimSight;
    [SerializeField] private float bareHandAttackPow = 7;

    private float lastShootTime = -Mathf.Infinity;


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
        }
    }

    private void DoMeleeAttack()
    {

        if (currentWeaponSlot == null || currentWeaponSlot.weaponProfile == null) // if not carrying any weapon, deal damage by the default stat of the character
        {
            DetectAndDealMeleeDamage(bareHandAttackPow);
            return;
        }
        int currentWeaponPow = currentWeaponSlot.weaponProfile.MeleeDamage;
        DetectAndDealMeleeDamage(currentWeaponPow);

    }

    private void DetectAndDealMeleeDamage(float damage)
    {
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(meleeAttackPoint.position, meleeAttackRange, enemyLayers);
        SlashFx slashEffect = Instantiate(slashEffectPrefab, meleeAttackPoint.position, meleeAttackPoint.rotation);
        slashEffect.gameObject.SetActive(true);
        slashEffect.SetTransform(meleeAttackPoint.transform);
        foreach (Collider2D enemy in hitEnemies)
        {
            EnemyDamageReceiver enemyDamageReceiver = enemy.GetComponent<EnemyDamageReceiver>();
            if (enemyDamageReceiver != null)
            {
                enemyDamageReceiver.TakeDamage((int)damage + (int)buffSys.GetBonus(BuffType.Attack));
                enemyDamageReceiver.GetKnockback((enemy.transform.position - meleeAttackPoint.position).normalized * 2);//add 2 for testing
            }
        }
    }


    private void DoRangedAttack()
    {
        if (!CanShoot()) return;
        BulletController newBullet = Instantiate(currentWeaponSlot.weaponProfile.BulletPrefab, currentWeaponSlot.transform.position, currentWeaponSlot.transform.rotation);
        newBullet.SetBulletStats(currentWeaponSlot.weaponProfile);
        Vector2 closestEnemyPos = aimSight.GetClosestEnemyPos();
        if (closestEnemyPos != Vector2.zero) newBullet.SetTarget(aimSight.GetClosestEnemyPos());
        else newBullet.SetRotation(InputManager.Instance.GetAttackDirVector());
        currentWeaponSlot.BulletDecrease();
        lastShootTime = Time.time;
    }

    private bool CanShoot()
    {
        if (currentWeaponSlot == null || currentWeaponSlot.weaponProfile == null) return false;
        WeaponProfile currentWeapon = currentWeaponSlot.weaponProfile;
        return lastShootTime + currentWeapon.AttackSpeed <= Time.time;

    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(meleeAttackPoint.transform.position, meleeAttackRange);
        Gizmos.color = Color.red;
    }

    protected override void LoadComponents()
    {

        if (enemyLayers == 0) enemyLayers = LayerMask.GetMask("EnemyDamageReceiver");
        if (weaponSlots.Count == 0) weaponSlots = new List<PlayerWeaponSlot>(GetComponentsInChildren<PlayerWeaponSlot>());

    }

    public void Init(PlayerCore core)
    {
        coreFighter = core;
        aimSight = core.AimSight;
        if (buffSys == null) buffSys = coreFighter.BuffMng;
        if (meleeAttackPoint == null) meleeAttackPoint = coreFighter.MeleeAttackPoint;
        if (slashEffectPrefab == null) slashEffectPrefab = meleeAttackPoint.Find("SlashEffect").GetComponent<SlashFx>();
    }
}
