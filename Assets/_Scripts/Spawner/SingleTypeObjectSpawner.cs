using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingleTypeObjectSpawner<T> : MonoBehaviour where T : MonoBehaviour
{
    public ObjectPool<T> objectPool;
    public Transform spawnPoint;

    private void OnValidate()
    {
        LoadComponents();
    }
    private void Start()
    {
        LoadComponents();
    }
    protected virtual void Spawn()
    {
        float spawnX = Random.Range(-8.88f, 8.88f);
        float spawnY = Random.Range(-5.004f, 5.00f);
        Vector2 spawnPos = new Vector2(spawnX, spawnY);
        T obj = objectPool.GetObjectFromPool();
        obj.transform.position = spawnPos;


    }
    private void LoadComponents()
    {
        if (objectPool == null) objectPool = GetComponentInChildren<ObjectPool<T>>();

    }
}
