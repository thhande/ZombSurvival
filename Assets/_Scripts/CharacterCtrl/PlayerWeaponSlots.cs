using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeaponSlots : WeaponContainer
{
    private SpriteRenderer weaponSpriteRenderer;

    private void OnValidate()
    {
        Start();
    }

    void Start()
    {
        LoadComponents();
    }

    void Update()
    {
        UpdateWeaponAnim();
    }

    public void BulletDecrease()
    {
        if (weaponProfile != null)
        {
            bulletCount--;
            if (bulletCount <= 0)
            {
                bulletCount = 0;
                RemoveWeapon();
            }
        }
    }
    public void RemoveWeapon()
    {
        weaponProfile = null;
        weaponSpriteRenderer.sprite = null;
    }

    //sprite renderer logic
    //#--------------------------------------------------
    private void UpdateWeaponVisual()
    {
        weaponSpriteRenderer.sprite = weaponProfile.weaponSprite;
    }
    private void UpdateWeaponAnim()
    {
        Vector2 direction = InputManager.instance.GetMovementVector().normalized;
        if (direction.x != 0) weaponSpriteRenderer.flipY = direction.x < 0;
        if (direction == Vector2.zero) return;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, angle);
    }

    public void HideWeaponSprite()
    {
        if (weaponSpriteRenderer != null) weaponSpriteRenderer.enabled = false;
    }
    public void ShowWeaponSprite()
    {
        weaponSpriteRenderer.enabled = true;
    }
    //#--------------------------------------------------
    //handle weapon profile logic
    //#--------------------------------------------------
    public void AddNewWeapon(WeaponDrop newWeapon)
    {
        if (weaponProfile == null)
        {
            weaponProfile = newWeapon.weaponProfile;
            bulletCount = newWeapon.bulletCount;


        }
        else if (weaponProfile != newWeapon.weaponProfile || (weaponProfile.weaponType == newWeapon.weaponProfile.weaponType && weaponProfile.weaponType == WeaponType.Melee))
        {
            weaponProfile = newWeapon.weaponProfile;
            bulletCount = newWeapon.bulletCount;
        }
        else
        {
            bulletCount += newWeapon.bulletCount;
        }

        UpdateWeaponVisual();
    }

    //load components logic
    //#--------------------------------------------------
    private void LoadComponents()
    {
        weaponSpriteRenderer = GetComponent<SpriteRenderer>();
        if (weaponProfile != null) weaponSpriteRenderer.sprite = weaponProfile.weaponSprite;
    }


}
