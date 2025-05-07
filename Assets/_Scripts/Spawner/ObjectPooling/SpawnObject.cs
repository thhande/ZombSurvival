using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnObject : MonoBehaviour
{
    protected ObjectPool objectPool;
    public void SetPool(ObjectPool pool)
    {
        this.objectPool = pool;
    }
}
