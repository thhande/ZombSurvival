using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    [SerializeField] private Queue<SpawnObject> objectPool = new Queue<SpawnObject>();
    private int poolSize = 10;
    [SerializeField] private SpawnObject prefab;
    private void Awake()
    {
        for (int i = 0; i < poolSize; i++)
        {
            SpawnObject newObject = Instantiate(prefab);
            objectPool.Enqueue(newObject);
            newObject.gameObject.SetActive(false);
        }
    }
    public SpawnObject GetObjectFromPool()
    {
        if (objectPool.Count == 0)
        {
            SpawnObject newObject = Instantiate(prefab);
            return newObject;
        }
        SpawnObject objectToSpawn = objectPool.Dequeue();
        objectToSpawn.gameObject.SetActive(true);
        return objectToSpawn;
    }
    public void ReturnObjectToPool(SpawnObject objectToReturn)
    {
        objectToReturn.gameObject.SetActive(false);
        objectPool.Enqueue(objectToReturn);
    }
}
