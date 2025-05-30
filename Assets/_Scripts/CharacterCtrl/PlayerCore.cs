using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class PlayerCore : MonoBehaviour
{

    [SerializeField] private PlayerAttack playerCombat;
    [SerializeField] private PlayerDamageReceiver damageReceiver;
    [SerializeField] private Animator playerVisual;
    [SerializeField] private RangedAttackRange rangedAttackRange;
    [SerializeField] private Transform meleeAttackPoint;
    [SerializeField] private PlayerMovement playerMovement;
    [SerializeField] private PlayerBuffs buffMng;



    public PlayerAttack PlayerCombat => playerCombat;
    public PlayerDamageReceiver DamageReceiver => damageReceiver;
    public Animator PlayerVisual => playerVisual;
    public RangedAttackRange RangedAttackRange => rangedAttackRange;
    public Transform MeleeAttackPoint => meleeAttackPoint;
    public PlayerMovement PlayerMovement => playerMovement;
    public PlayerBuffs BuffMng => buffMng;






    private void Awake()
    {
        LoadComponents();
    }

#if UNITY_EDITOR
    private void OnValidate()
    {
        LoadComponents();
    }
#endif

    private void LoadComponents()
    {
        if (playerCombat == null) playerCombat = transform.GetComponentInChildren<PlayerAttack>();
        playerCombat.Init(this);
        if (damageReceiver == null) damageReceiver = transform.GetComponentInChildren<PlayerDamageReceiver>();
        if (playerVisual == null) playerVisual = transform.GetComponentInChildren<Animator>();
        if (rangedAttackRange == null) rangedAttackRange = transform.GetComponentInChildren<RangedAttackRange>();
        if (meleeAttackPoint == null) meleeAttackPoint = transform.Find("MeleeAttackPoint");
        if (playerMovement == null) playerMovement = GetComponent<PlayerMovement>(); playerMovement.Init(this);
        if (buffMng == null) buffMng = transform.GetComponentInChildren<PlayerBuffs>();
    }


}
