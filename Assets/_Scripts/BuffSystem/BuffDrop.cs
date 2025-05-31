using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class BuffDrop : MonoBehaviour
{
    public SpawnObject spawnObject;
    public BuffData buffData;

    private void OnValidate()
    {
#if UNITY_EDITOR
        spawnObject = GetComponent<SpawnObject>();
#endif
    }
    private void Awake()
    {
        spawnObject = GetComponent<SpawnObject>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        PlayerBuffs playerBuff = other.GetComponent<PlayerBuffs>();
        if (playerBuff != null)
        {
            playerBuff.AddNewBuff(buffData);
            spawnObject.ReturnToPool();
        }
    }
}
