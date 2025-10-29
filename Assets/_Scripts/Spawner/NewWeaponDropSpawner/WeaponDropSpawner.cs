using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class WeaponDropSpawner : SingleTypeObjectSpawner
{
    [SerializeField] private ObjectPool objectPool;
    [SerializeField] private float spawnRate = 10f;
    [SerializeField] List<WeaponProfile> weaponProfiles = new List<WeaponProfile>();
    private int minSpawnCount = 2;
    private int maxSpawnCount = 15;


    private void Start()
    {
        LoadComponents();
        SpawnAtRandomPos();
        StartCoroutine(StartSpawn());
    }

    private IEnumerator StartSpawn()
    {
        while (true)
        {
            yield return new WaitForSeconds(spawnRate);
            for (int i = 0; i < Random.Range(minSpawnCount, maxSpawnCount); i++)
            {
                SpawnAtRandomPos();
            }
        }
    }
    protected override void SpawnAtRandomPos()
    {
        Vector2 spawnPos = GenerateRandomPos();
        WeaponSpawnObj obj = (WeaponSpawnObj)objectPool.GetObjectFromPool();
        obj.transform.position = spawnPos;
        WeaponProfile weaponProfile = weaponProfiles[Random.Range(0, weaponProfiles.Count)];
        int newBulletCount = GenerateRandomBulletCount(weaponProfile);
        obj.OnSpawn(weaponProfile, newBulletCount);
        obj.SetPool(this.objectPool);
    }

    private Vector2 GenerateRandomPos()//weapon may drop randomly near or far player
    {
        Vector2 spawnPos;
        int randomNum = Random.Range(0, 2);
        if (randomNum == 0) spawnPos = GetRandomPositionAroundCamera();
        else spawnPos = GetRandomPositionAroundWorld();
        return spawnPos;
    }

    private int GenerateRandomBulletCount(WeaponProfile newWeaponProfile)
    {

        if (newWeaponProfile.WeaponType == WeaponType.Melee)
        {
            return Random.Range(20, 30);
        }
        else return Random.Range(10, 50);

    }


    protected override void LoadComponents()
    {
        objectPool = transform.GetComponentInChildren<ObjectPool>();
    }


}
