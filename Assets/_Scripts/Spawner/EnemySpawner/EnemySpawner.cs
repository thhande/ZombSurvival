using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemySpawner : MultiTypeObjectSpawner
{

    [SerializeField] private float spawnDelay = 10f;
    [SerializeField] private int minSpawnAmount = 1;
    [SerializeField] private int maxSpawnAmount = 5;


    protected override void Start()
    {
        base.Start();
        StartCoroutine(SpawnEnemyCoroutine());

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

    IEnumerator SpawnEnemyCoroutine()
    {
        while (true)
        {
            UpdateSpawnCountByWave();
            int spawnNum = Random.Range(minSpawnAmount, maxSpawnAmount + 1);
            SpawnEnemy(spawnNum);
            GameManager.instance.EnemyWaveUpdate();
            yield return new WaitForSeconds(spawnDelay);

        }
    }
    private void UpdateSpawnCountByWave()
    {
        if (GameManager.instance.wave != 0 && GameManager.instance.wave % 10 == 0)
        {
            maxSpawnAmount++;
            spawnDelay -= 10 / (GameManager.instance.wave * 2);
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
