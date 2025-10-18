using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class BuffDrop : MMono
{
    public SpawnObject spawnObject;
    public BuffData buffData;

    private void OnTriggerEnter2D(Collider2D other)
    {
        PlayerBuffs playerBuff = other.GetComponent<PlayerBuffs>();
        if (playerBuff != null)
        {
            playerBuff.AddNewBuff(buffData);
            spawnObject.ReturnToPool();
        }
    }

    protected override void LoadComponents()
    {
        base.LoadComponents();
        if (spawnObject == null) spawnObject = GetComponent<SpawnObject>();
    }
}
