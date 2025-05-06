using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnObject<T> : MonoBehaviour where T : SpawnObject<T>
{
    private ObjectPool<T> objectPool;
    public void SetPool(ObjectPool<T> pool)
    {
        this.objectPool = pool;
    }
}
