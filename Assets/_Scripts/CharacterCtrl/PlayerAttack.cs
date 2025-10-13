using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class PlayerAttack : MonoBehaviour, IData
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

    const float RANGED_ATTACK_RANGE = 7.06f;
#if UNITY_EDITOR
    private void OnValidate()
    {
        LoadComponents();
    }
#endif
    private void Start()
    {
        LoadComponents();
    }

    private void Update()
    {
        HandleInput();
        UpdateAttackPointPosition();

    }


    private void HandleInput()
    {
        if (InputManager.instance.GetMeleeAttackInput()) DoMeleeAttack();
        else if (InputManager.instance.GetRangedAttackInput()) DoRangedAttack();
        if (InputManager.instance.SwitchToWeaponSlotOne()) SwitchToWeaponSlot(0);
        else if (InputManager.instance.SwitchToWeaponSlotTwo()) SwitchToWeaponSlot(1);
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
        if (InputManager.instance.GetMovementVector() != Vector2.zero)
        {
            meleeAttackPoint.localPosition = InputManager.instance.GetMovementVector() * 0.5f + Vector2.down * 0.25f;
            if (!InputManager.instance.GetRangedAttackInput()) rangedAttackRange.transform.localPosition = InputManager.instance.GetMovementVector() * 0 * RANGED_ATTACK_RANGE;
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
        if (currentWeaponSlot == null || currentWeaponSlot.weaponProfile == null) return;
        BulletMoving newBullet = Instantiate(currentWeaponSlot.weaponProfile.bulletPrefab, currentWeaponSlot.transform.position, currentWeaponSlot.transform.rotation);
        EnemyDamageReceiver closestEnemy = rangedAttackRange.FindClosestEnemy();
        if (closestEnemy != null) newBullet.SetTarget(closestEnemy.transform.position);
        currentWeaponSlot.BulletDecrease();

    }

    private void LoadComponents()
    {

        // if (meleeAttackPoint == null) meleeAttackPoint = transform.parent.Find("MeleeAttackPoint");
        // if (rangedAttackRange == null) rangedAttackRange = transform.parent.GetComponentInChildren<RangedAttackRange>();
        if (enemyLayers == 0) enemyLayers = LayerMask.GetMask("EnemyDamageReceiver");

        if (weaponSlots.Count == 0) weaponSlots = new List<PlayerWeaponSlot>(GetComponentsInChildren<PlayerWeaponSlot>());

    }

    public void Init(PlayerCore core)
    {
        coreFighter = core;
        if (buffSys == null) buffSys = coreFighter.BuffMng;
        if (meleeAttackPoint == null) meleeAttackPoint = coreFighter.MeleeAttackPoint;
        if (slashEffectPrefab == null) slashEffectPrefab = meleeAttackPoint.Find("SlashEffect").GetComponent<SlashFx>();
        if (rangedAttackRange == null) rangedAttackRange = coreFighter.RangedAttackRange;
    }
}
