using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCore : CoreBase<EnemyCore>
{
    [SerializeField] private EnemyDamageReceiver damageReceiver;
    [SerializeField] private EnemyDamageSender damageSender;
    [SerializeField] private Knockback knockbackSys;
    [SerializeField] private SpawnObject spawnCtrl;
    [SerializeField] private SpriteRenderer visual;
    [SerializeField] private Animator anim;

    public EnemyDamageReceiver DamageReceiver => damageReceiver;
    public EnemyDamageSender DamageSender => damageSender;
    public Knockback KnockbackSys => knockbackSys;
    public SpawnObject SpawnCtrl => spawnCtrl;
    public SpriteRenderer Visual => visual;
    public Animator Anim => anim;

    protected override void LoadComponents()
    {

        if (knockbackSys == null) knockbackSys = GetComponent<Knockback>();
        if (spawnCtrl == null) spawnCtrl = GetComponent<SpawnObject>();
        if (visual == null) visual = transform.GetComponentInChildren<SpriteRenderer>();
        if (anim == null) anim = transform.GetComponentInChildren<Animator>();
        LoadComponent(ref damageReceiver, true);
        LoadComponent(ref damageSender, true);

    }



}
