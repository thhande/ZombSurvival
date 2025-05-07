using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSpawnObj : SpawnObject
{
    [SerializeField] private WeaponDropContainer container;
    [SerializeField] private float lifeTime = 5f;
    private void Awake()
    {
        LoadComponents();
    }
    private void OnValidate()
    {
        LoadComponents();
    }
    private void LoadComponents()
    {
        container = GetComponent<WeaponDropContainer>();

    }
    public void OnSpawn(WeaponProfile newWeaponProfile, int newBulletCount)
    {
        container.OnSpawn(newWeaponProfile, newBulletCount);
    }
    IEnumerator ReturnToPoolAfterTime()
    {
        yield return new WaitForSeconds(lifeTime);
        objectPool.ReturnObjectToPool(this);
    }
    private void OnEnable()
    {
        StartCoroutine(ReturnToPoolAfterTime());
    }
}
