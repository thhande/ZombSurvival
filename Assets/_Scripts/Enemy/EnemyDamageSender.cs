using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamageSender : DamageSender
{
    [SerializeField] private Knockback knockbackSys;

    public EnemyDamageSender()
    {
        damage = 10;
    }
    protected override void DealDamage(Collider2D collision)
    {
        base.DealDamage(collision);
        PlayerDamageReceiver hitPlayer = collision.GetComponent<PlayerDamageReceiver>();
        if (hitPlayer != null)
        {
            hitPlayer.TakeDamage(damage);
            Debug.Log("Deal " + damage + " damage to player");


            Vector2 knockbackDir = (transform.position - collision.transform.position).normalized;
            knockbackSys.ApplyKnockback(knockbackDir);
        }

    }

    private void OnValidate()
    {
        LoadComponents();
    }
    private void LoadComponents()
    {
        if (knockbackSys == null) knockbackSys = transform.parent.GetComponent<Knockback>();
    }
}
