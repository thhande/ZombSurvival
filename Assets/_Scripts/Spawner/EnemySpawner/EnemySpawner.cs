using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MultiTypeObjectSpawner
{



    protected override void Start()
    {
        base.Start();
        SpawnEnemy(1);
    }

    private ObjectPool GetRandomEnemyPoolAuto()
    {
        var validTags = new List<ObjectTag>();

        foreach (ObjectTag tag in System.Enum.GetValues(typeof(ObjectTag)))
        {
            if (tag.ToString().StartsWith("Enemy") && objectPoolDictionary.ContainsKey(tag))
            {
                validTags.Add(tag);
            }
        }

        if (validTags.Count == 0) return null;

        ObjectTag randomTag = validTags[Random.Range(0, validTags.Count)];
        return objectPoolDictionary[randomTag];
    }

    IEnumerator SpawnEnemyCoroutine(int amount, float delay) //just using magic number for testing
    {
        while (true)
        {
            int spawnNum = Random.Range(1, 5);
            SpawnEnemy(spawnNum);
            yield return new WaitForSeconds(5f);
        }
    }
    private void SpawnEnemy(int amount)
    {
        for (int i = 0; i < amount; i++)
        {
            ObjectPool enemyPool = GetRandomEnemyPoolAuto();
            SpawnObject enemy = enemyPool.GetObjectFromPool();
            enemy.transform.position = GetRandomPosition();

        }
    }
}
