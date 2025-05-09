using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDamageReceiver : DamageReceiver
{
    public PlayerDamageReceiver()
    {
        health = 40;
        maxHealth = 40;
    }
    protected override void Die()
    {
        base.Die();
        transform.parent.gameObject.SetActive(false);
    }
}
