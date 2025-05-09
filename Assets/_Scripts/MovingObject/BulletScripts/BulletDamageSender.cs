using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class BulletDamageSender : DamageSender
{
    public BulletDamageSender()
    {
        damage = 10;
    }

    protected override void DealDamage(Collider2D collision)
    {
        base.DealDamage(collision);
        Debug.Log("Hit something");
        EnemyDamageReceiver damageReceiver = collision.GetComponent<EnemyDamageReceiver>();
        if (damageReceiver != null)
        {
            damageReceiver.TakeDamage(damage);
            Debug.Log("Deal " + damage + " damage to something");
            Destroy(gameObject);
        }

    }
}
