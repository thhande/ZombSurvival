using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class WeaponDropSpawner : SingleTypeObjectSpawner
{
    [SerializeField] private ObjectPool objectPool;
    [SerializeField] private float spawnRate = 10f;
    [SerializeField] List<WeaponProfile> weaponProfiles = new List<WeaponProfile>();
    private int minSpawnCount = 1;
    private int maxSpawnCount = 5;
    private void OnValidate()
    {
        LoadComponents();
    }

    private void Start()
    {
        LoadComponents();
        SpawnAtRandomPos();
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

            Debug.Log("Spawned at: " + objectPool.transform.position);
        }
    }
    protected override void SpawnAtRandomPos()
    {
        Vector2 spawnPos = GetRandomPosition();
        WeaponSpawnObj obj = (WeaponSpawnObj)objectPool.GetObjectFromPool();
        obj.transform.position = spawnPos;
        WeaponProfile weaponProfile = weaponProfiles[Random.Range(0, weaponProfiles.Count)];
        int newBulletCount = GenerateRandomBulletCount(weaponProfile);
        obj.OnSpawn(weaponProfile, newBulletCount);
    }
    private int GenerateRandomBulletCount(WeaponProfile newWeaponProfile)
    {

        if (newWeaponProfile.weaponType == WeaponType.Melee)
        {
            return 1;
        }
        else return Random.Range(10, 50);

    }


    private void LoadComponents()
    {
        objectPool = transform.GetComponentInChildren<ObjectPool>();
    }


}
