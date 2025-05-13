using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyDamageReceiver : DamageReceiver
{
    [SerializeField] private SpawnObject spawnObject;
    [SerializeField] private Knockback knockbackSys;
    [SerializeField] private Animator anim;
    public EnemyDamageReceiver()
    {
        health = 10;
        maxHealth = 10;
    }
    private void OnValidate()
    {
        LoadComponents();
    }
    protected override void Die()
    {
        base.Die();
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
        LoadComponents();
    }

    private void LoadComponents()
    {
        if (knockbackSys == null) knockbackSys = transform.parent.GetComponent<Knockback>();
        spawnObject = transform.parent.GetComponent<SpawnObject>();
        if (anim == null) anim = transform.parent.GetComponentInChildren<Animator>();
    }


}
