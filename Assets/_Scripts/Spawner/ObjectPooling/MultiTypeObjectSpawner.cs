using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MultiTypeObjectSpawner : MonoBehaviour
{
    [SerializeField] protected List<ObjectPool> objectPools = new List<ObjectPool>();
    protected Dictionary<ObjectTag, ObjectPool> objectPoolDictionary = new Dictionary<ObjectTag, ObjectPool>();
    protected virtual void Awake()
    {
        LoadComponents();
        foreach (ObjectPool pool in objectPools)
        {
            objectPoolDictionary.Add(pool.objectTag, pool);
        }
    }
    protected virtual void Start()
    {
        LoadComponents();
    }

    protected void OnValidate()
    {
        LoadComponents();
    }

    protected virtual void LoadComponents()
    {
        if (objectPools.Count == 0) objectPools = new List<ObjectPool>(GetComponentsInChildren<ObjectPool>());
    }
    protected virtual void SpawnObject(int amount) { }

    protected Vector2 GetRandomPosition()
    {
        // Get the bottom left and top right corners of the camera view in world coordinates
        Vector3 bottomLeft = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, 0));

        Vector3 topRight = Camera.main.ViewportToWorldPoint(new Vector3(1, 1, 0));


        float padding = 0.5f;


        float spawnX = Random.Range(bottomLeft.x + padding, topRight.x - padding);
        float spawnY = Random.Range(bottomLeft.y + padding, topRight.y - padding);
        return new Vector2(spawnX, spawnY);
    }
}
