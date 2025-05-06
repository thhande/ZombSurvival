using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool<T> : MonoBehaviour where T : MonoBehaviour
{
    public Queue<T> objectPool = new Queue<T>();
    public int poolSize = 10;
    public T prefab;
    private void Awake()
    {
        for (int i = 0; i < poolSize; i++)
        {
            T newObject = Instantiate(prefab);
            objectPool.Enqueue(newObject);
            newObject.gameObject.SetActive(false);
        }
    }
    public T GetObjectFromPool()
    {
        if (objectPool.Count == 0)
        {
            T newObject = Instantiate(prefab);
            return newObject;
        }
        T objectToSpawn = objectPool.Dequeue();
        objectToSpawn.gameObject.SetActive(true);
        return objectToSpawn;
    }
    public void ReturnObjectToPool(T objectToReturn)
    {
        objectToReturn.gameObject.SetActive(false);
        objectPool.Enqueue(objectToReturn);
    }
}
