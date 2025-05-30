using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyDamageReceiver : DamageReceiver, IData
{
    [SerializeField] private SpawnObject spawnObject;
    [SerializeField] private Knockback knockbackSys;
    [SerializeField] private Animator anim;
    [SerializeField] private int reward = 5;
    [SerializeField] private EnemyCore core;
    public EnemyDamageReceiver()
    {
        health = 10;
        maxHealth = 10;
    }
    // private void OnValidate()
    // {
    //     LoadComponents();
    // }
    protected override void Die()
    {
        base.Die();
        Reset();
        GameManager.instance.AddScore(reward);
        if (spawnObject != null) spawnObject.ReturnToPool();

    }

    public override void TakeDamage(int damage)
    {
        base.TakeDamage(damage);
        Debug.Log("Deal " + damage + " damage to enemy");
        anim.SetTrigger("Hit");
    }

    public void GetKnockback(Vector2 knockbackDir)
    {
        knockbackSys.ApplyKnockback(knockbackDir);
    }
    private void Start()
    {
        // LoadComponents();
    }

    private void LoadComponents()
    {
        if (knockbackSys == null) knockbackSys = transform.parent.GetComponent<Knockback>();
        spawnObject = transform.parent.GetComponent<SpawnObject>();
        if (anim == null) anim = transform.parent.GetComponentInChildren<Animator>();
    }

    public void Init(EnemyCore Core)
    {
        core = Core;
        anim = core.Anim;
        knockbackSys = core.KnockbackSys;
        spawnObject = core.SpawnCtrl;
    }


}
