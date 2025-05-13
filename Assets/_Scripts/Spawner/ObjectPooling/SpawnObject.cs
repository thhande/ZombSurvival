using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnObject : MonoBehaviour //control the link between the object and the pool
{
    protected ObjectPool objectPool;
    public void SetPool(ObjectPool pool)
    {
        this.objectPool = pool;
    }
    public void ReturnToPool()
    {
        StopAllCoroutines();
        if (objectPool != null) objectPool.ReturnObjectToPool(this);

    }
}
