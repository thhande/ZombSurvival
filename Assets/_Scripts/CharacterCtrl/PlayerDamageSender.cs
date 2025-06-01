using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(CircleCollider2D))]
public class PlayerDamageSender : DamageSender, IData
{

    [SerializeField] private float knockForce = 1;
    [SerializeField] private PlayerCore core;
    [SerializeField] private PlayerBuffs buff;

    protected override void DealDamage(Collider2D other)
    {

        if (buff.GetBonus(BuffType.Speed) <= 0) return;
        if (other.gameObject.layer == LayerMask.NameToLayer("EnemyDamageReceiver"))
        {
            EnemyDamageReceiver hitEnemy = other.GetComponent<EnemyDamageReceiver>();
            hitEnemy.TakeDamage(damage);
            hitEnemy.GetKnockback((hitEnemy.transform.position - transform.position) * knockForce);


        }
    }

    public void Init(PlayerCore Core)
    {
        this.core = Core;
        this.buff = core.BuffMng;
    }
}
