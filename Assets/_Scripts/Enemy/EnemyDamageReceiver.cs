using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamageReceiver : DamageReceiver
{
    [SerializeField] private SpawnObject spawnObject;
    public EnemyDamageReceiver()
    {
        health = 10;
        maxHealth = 10;
    }
    private void OnValidate()
    {
        spawnObject = transform.parent.GetComponent<SpawnObject>();
    }
    protected override void Die()
    {
        base.Die();
        spawnObject.ReturnToPool();
    }

}
