using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponDropContainer : WeaponContainer
{
    [SerializeField] private SpriteRenderer spriteRenderer;
    private void Awake()
    {
        StartCoroutine(AutoDestroy());
    }
    private void OnValidate()
    {
        LoadComponents();
    }
    private void Start()
    {
        LoadComponents();
    }
    public void IsPickedUp()
    {
        Destroy(gameObject);
    }

    private void LoadComponents()
    {
        if (spriteRenderer == null) spriteRenderer = GetComponent<SpriteRenderer>();
    }
    public void OnSpawn(WeaponProfile newWeaponProfile, int newBulletCount)
    {
        weaponProfile = newWeaponProfile;
        spriteRenderer.sprite = weaponProfile.weaponSprite;
        bulletCount = newBulletCount;
    }
    IEnumerator AutoDestroy()
    {
        yield return new WaitForSeconds(5f);
        Destroy(gameObject);
    }


}
