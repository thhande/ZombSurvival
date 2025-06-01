using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDamageReceiver : DamageReceiver, IData
{
    public event System.Action OnHealthChange;


    [SerializeField] private PlayerCore core;
    [SerializeField] private PlayerBuffs buff;



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
        if (buff.GetBonus(BuffType.Invincible) > 0) return;
        base.TakeDamage(damage);
        OnHealthChange?.Invoke();

    }


    public void Init(PlayerCore Core)
    {
        core = Core;
        buff = core.BuffMng;
    }

}
