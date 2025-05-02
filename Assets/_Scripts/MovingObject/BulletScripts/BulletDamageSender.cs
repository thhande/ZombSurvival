using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class BulletDamageSender : MonoBehaviour
{
    [SerializeField] private int damage = 5;
    private void OnTriggerEnter2D(Collider2D collision)
    {
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
