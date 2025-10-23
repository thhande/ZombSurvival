using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffSpawner : SingleTypeObjectSpawner
{
    [SerializeField] ObjectPool pool;
    [SerializeField] List<BuffData> buffDatas = new List<BuffData>();


    private void Start()
    {
        StartCoroutine(SpawnCoroutine());
    }

    IEnumerator SpawnCoroutine()
    {
        while (GameManager.instance.isGameActive)
        {
            yield return new WaitForSeconds(10);
            Spawn();
        }
    }

    private void Spawn()
    {
        BuffData randomBuff = buffDatas[Random.Range(0, buffDatas.Count)];
        BuffSpawnObject buffDrop = (BuffSpawnObject)pool.GetObjectFromPool();
        buffDrop.buffDrop.buffData = randomBuff;
        buffDrop.transform.position = GetRandomPositionAroundWorld();
    }

    protected override void Awake()
    {
        pool = transform.GetComponentInChildren<ObjectPool>();
    }

}
