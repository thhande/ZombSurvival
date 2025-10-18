using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeaponSlot : WeaponContainer
{
    [SerializeField] private PlayerWeaponVisual weaponVisual;

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
        weaponVisual.UpdateWeaponAnim();
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
            else UIInputManager.Instance.UpdateWeaponBulletCount(this.transform.GetSiblingIndex(), bulletCount);
        }
    }
    public void RemoveWeapon()
    {
        weaponProfile = null;
        weaponVisual.RemoveSprite();
        UIInputManager.Instance.OnWeaponRemoved(this.transform.GetSiblingIndex());
    }


    public void AddNewWeapon(WeaponDropContainer newWeapon)
    {
        if (weaponProfile == null)
        {
            weaponProfile = newWeapon.weaponProfile;
            bulletCount = newWeapon.bulletCount;
            newWeapon.IsPickedUp();


        }
        else if (weaponProfile != newWeapon.weaponProfile || (weaponProfile.weaponType == newWeapon.weaponProfile.weaponType && weaponProfile.weaponType == WeaponType.Melee))
        {
            WeaponProfile oldWeapon = weaponProfile;
            int oldBulletCount = bulletCount;
            weaponProfile = newWeapon.weaponProfile;
            bulletCount = newWeapon.bulletCount;
            newWeapon.UpdateInfoAndVisual(oldWeapon, oldBulletCount);
        }
        else
        {
            bulletCount += newWeapon.bulletCount;
            newWeapon.IsPickedUp();
        }

        weaponVisual.UpdateWeaponVisual();
        UIInputManager.Instance.UpdateWeaponBulletCount(this.transform.GetSiblingIndex(), bulletCount);
        UIInputManager.Instance.SetWeaponSlotSprite(this.transform.GetSiblingIndex(), weaponProfile.weaponSprite);
    }

    //load components logic
    //#--------------------------------------------------
    private void LoadComponents()
    {
        if (weaponVisual == null) weaponVisual = GetComponentInChildren<PlayerWeaponVisual>();
        if (weaponProfile != null) weaponVisual.UpdateWeaponVisual();
    }

    public void ShowWeapon()
    {
        weaponVisual.ShowWeaponSprite();
    }

    public void HideWeapon()
    {
        weaponVisual.HideWeaponSprite();
    }


}
