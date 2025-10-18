using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class PlayerCore : CoreBase<PlayerCore>
{

    [SerializeField] private PlayerAttack playerCombat;
    [SerializeField] private PlayerDamageReceiver damageReceiver;
    [SerializeField] private Animator playerVisual;
    [SerializeField] private Transform meleeAttackPoint;
    [SerializeField] private PlayerMovement playerMovement;
    [SerializeField] private PlayerDamageSender damageSender;
    [SerializeField] private PlayerBuffs buffMng;



    public PlayerAttack PlayerCombat => playerCombat;
    public PlayerDamageReceiver DamageReceiver => damageReceiver;
    public Animator PlayerVisual => playerVisual;
    public Transform MeleeAttackPoint => meleeAttackPoint;
    public PlayerMovement PlayerMovement => playerMovement;
    public PlayerDamageSender DamageSender => damageSender;
    public PlayerBuffs BuffMng => buffMng;





    protected override void LoadComponents()
    {
        if (playerVisual == null) playerVisual = transform.GetComponentInChildren<Animator>();
        if (meleeAttackPoint == null) meleeAttackPoint = transform.Find("MeleeAttackPoint");
        LoadComponent(ref playerCombat, true);
        LoadComponent(ref damageReceiver, true);
        LoadComponent(ref damageSender, true);
        LoadComponent(ref playerMovement);
        LoadComponent(ref buffMng);


    }


}
