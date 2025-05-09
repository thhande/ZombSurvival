using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSpawnObj : SpawnObject
{
    [SerializeField] private WeaponDropContainer container;
    [SerializeField] private float lifeTime = 5f;

    public void OnSpawn(WeaponProfile newWeaponProfile, int newBulletCount)
    {
        container.UpdateInfoAndVisual(newWeaponProfile, newBulletCount);
    }
    IEnumerator ReturnToPoolAfterTime()
    {
        yield return new WaitForSeconds(lifeTime);
        objectPool.ReturnObjectToPool(this);
    }
    public void ReturnToPool()
    {
        StopAllCoroutines();
        objectPool.ReturnObjectToPool(this);

    }
    private void Awake()
    {
        LoadComponents();
    }
    private void OnValidate()
    {
        LoadComponents();
    }
    private void OnEnable()
    {
        StartCoroutine(ReturnToPoolAfterTime());
    }
    private void LoadComponents()
    {
        container = GetComponent<WeaponDropContainer>();

    }

}
