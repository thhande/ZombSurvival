using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MultiTypeObjectSpawner : ObjectSpawner
{
    [SerializeField] protected List<ObjectPool> objectPools = new List<ObjectPool>();
    protected Dictionary<ObjectTag, ObjectPool> objectPoolDictionary = new Dictionary<ObjectTag, ObjectPool>();




    protected virtual void SpawnObject(int amount) { }


    private void LoadObjectPools()
    {
        foreach (ObjectPool pool in objectPools)
        {
            objectPoolDictionary.Add(pool.objectTag, pool);
        }
    }

    protected override void Awake()
    {
        base.LoadComponents();

        LoadObjectPools();
    }

    protected virtual void Start()
    {
        LoadComponents();
    }

    protected override void LoadComponents()
    {
        if (objectPools.Count == 0) objectPools = new List<ObjectPool>(GetComponentsInChildren<ObjectPool>());
    }
}
