using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDropSpawner : MonoBehaviour
{
    [SerializeField] private List<WeaponProfile> weaponProfiles = new List<WeaponProfile>();
    [SerializeField] private WeaponDropContainer weaponDropPrefab;
    public float spawnRate = 10f;
    private void Start()
    {
        StartCoroutine(StartSpawning());
    }

    private void Spawn()
    {
        float spawnX = Random.Range(-8.88f, 8.88f);
        float spawnY = Random.Range(-5.004f, 5.00f);
        Vector2 spawnPos = new Vector2(spawnX, spawnY);
        WeaponDropContainer obj = Instantiate(weaponDropPrefab, spawnPos, Quaternion.identity);
        WeaponProfile newWeaponProfile = weaponProfiles[Random.Range(0, weaponProfiles.Count)];
        int newBulletCount;
        if (newWeaponProfile.weaponType == WeaponType.Melee)
        {
            newBulletCount = 0;
        }
        else newBulletCount = Random.Range(1, 20);
        obj.OnSpawn(newWeaponProfile, newBulletCount);

    }
    IEnumerator StartSpawning()
    {
        while (true)
        {
            yield return new WaitForSeconds(spawnRate);
            Spawn();
        }
    }


}
