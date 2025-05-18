using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDamageReceiver : DamageReceiver
{
    public event System.Action OnHealthChange;

    private void Start()
    {
        health = 40;
        maxHealth = 40;
        OnHealthChange?.Invoke();
    }

    protected override void Die()
    {
        base.Die();
        transform.parent.gameObject.SetActive(false);
        GameManager.instance.GameOver();
    }
    public override void TakeDamage(int damage)
    {
        base.TakeDamage(damage);
        OnHealthChange?.Invoke();

    }

}
