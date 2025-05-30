using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCore : MonoBehaviour
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

    private void LoadComponents()
    {
        if (damageReceiver == null) damageReceiver = transform.GetComponentInChildren<EnemyDamageReceiver>(); damageReceiver.Init(this);
        if (damageSender == null) damageSender = transform.GetComponentInChildren<EnemyDamageSender>(); damageSender.Init(this);
        if (knockbackSys == null) knockbackSys = GetComponent<Knockback>();
        if (spawnCtrl == null) spawnCtrl = GetComponent<SpawnObject>();
        if (visual == null) visual = transform.GetComponentInChildren<SpriteRenderer>();
        if (anim == null) anim = transform.GetComponentInChildren<Animator>();


    }

    private void Awake()
    {
        LoadComponents();
    }
    private void OnValidate()
    {
#if UNITY_EDITOR
        LoadComponents();
#endif
    }

}
