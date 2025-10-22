using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponDropContainer : WeaponContainer
{
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private WeaponSpawnObj weaponSpawnObj;

    private void Start()
    {
        LoadComponents();
    }
    public void IsPickedUp()
    {
        weaponSpawnObj.ReturnToPool();
    }

    protected override void LoadComponents()
    {
        if (spriteRenderer == null) spriteRenderer = GetComponent<SpriteRenderer>();
        if (weaponSpawnObj == null) weaponSpawnObj = GetComponent<WeaponSpawnObj>();
    }
    public void UpdateInfoAndVisual(WeaponProfile newWeaponProfile, int newBulletCount)
    {
        weaponProfile = newWeaponProfile;
        spriteRenderer.sprite = weaponProfile.weaponSprite;
        bulletCount = newBulletCount;
    }


}
