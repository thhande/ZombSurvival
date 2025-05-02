using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{

    [SerializeField] private List<PlayerWeaponSlots> weaponSlots = new List<PlayerWeaponSlots>();
    [SerializeField] private PlayerWeaponSlots currentWeaponSlot;
    [SerializeField] private Transform meleeAttackPoint;
    [SerializeField] private LayerMask enemyLayers;
    [SerializeField] private float attackRange = 0.5f;
    [SerializeField] private SlashFxAutoDestroy slashEffectPrefab;

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
        if (InputManager.instance.SwitchToWeaponSlotTwo()) SwitchToWeaponSlot(1);
    }

    private void SwitchToWeaponSlot(int slotIndex)
    {

        if (slotIndex < 0 || slotIndex >= weaponSlots.Count || currentWeaponSlot == weaponSlots[slotIndex]) return;
        if (currentWeaponSlot != null) currentWeaponSlot.HideWeaponSprite();
        currentWeaponSlot = weaponSlots[slotIndex];
        currentWeaponSlot.ShowWeaponSprite();
    }
    private void UpdateAttackPointPosition()
    {
        if (InputManager.instance.GetMovementVector() != Vector2.zero)
        {
            meleeAttackPoint.localPosition = InputManager.instance.GetMovementVector() * 0.5f + Vector2.down * 0.25f;
        }
    }


    private void DoMeleeAttack()
    {

        if (currentWeaponSlot == null || currentWeaponSlot.weaponProfile == null) return;
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(meleeAttackPoint.position, attackRange, enemyLayers);
        SlashFxAutoDestroy slashEffect = Instantiate(slashEffectPrefab, meleeAttackPoint.position, meleeAttackPoint.rotation);
        slashEffect.gameObject.SetActive(true);
        slashEffect.SetTransform(meleeAttackPoint.transform);
        foreach (Collider2D enemy in hitEnemies)
        {
            EnemyDamageReceiver enemyDamageReceiver = enemy.GetComponent<EnemyDamageReceiver>();
            if (enemyDamageReceiver != null)
            {
                enemyDamageReceiver.TakeDamage(currentWeaponSlot.weaponProfile.meleeDamage);
                Debug.Log("Enemy got " + currentWeaponSlot.weaponProfile.meleeDamage + " damage!");
            }
        }

    }

    private void DoRangedAttack()
    {
        if (currentWeaponSlot == null || currentWeaponSlot.weaponProfile == null) return;
        Instantiate(currentWeaponSlot.weaponProfile.bulletPrefab, currentWeaponSlot.transform.position, currentWeaponSlot.transform.rotation);
        currentWeaponSlot.BulletDecrease();

    }

    private void LoadComponents()
    {
        weaponSlots = new List<PlayerWeaponSlots>(GetComponentsInChildren<PlayerWeaponSlots>());
        meleeAttackPoint = transform.parent.Find("AttackPoint");
        enemyLayers = LayerMask.GetMask("EnemyDamageReceiver");
        slashEffectPrefab = meleeAttackPoint.Find("SlashEffect").GetComponent<SlashFxAutoDestroy>();

    }
}
