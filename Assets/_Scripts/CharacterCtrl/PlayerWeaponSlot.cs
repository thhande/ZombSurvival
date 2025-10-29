using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeaponSlot : WeaponContainer
{
    [SerializeField] private PlayerWeaponVisual weaponVisual;

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



    public void GetDropWeapon(WeaponDropContainer newWeapon)
    {
        if (weaponProfile == null) GetNewWeapon(newWeapon);

        else if (weaponProfile != newWeapon.weaponProfile)
        {
            SwapCurrentWeaponWithWeaponDrop(newWeapon);
        }
        else
        {
            bulletCount += newWeapon.bulletCount;  // increase bullet count if new weapon is the same with current
            newWeapon.IsPickedUp();
        }
        weaponVisual.UpdateWeaponVisual();
        UIInputManager.Instance.UpdateWeaponBulletCount(this.transform.GetSiblingIndex(), bulletCount);
        UIInputManager.Instance.SetWeaponSlotSprite(this.transform.GetSiblingIndex(), weaponProfile.WeaponSprite);
    }
    private void GetNewWeapon(WeaponDropContainer newWeapon)
    {
        weaponProfile = newWeapon.weaponProfile;
        bulletCount = newWeapon.bulletCount;
        newWeapon.IsPickedUp();
    }

    private void SwapCurrentWeaponWithWeaponDrop(WeaponDropContainer weaponDrop)
    {
        WeaponProfile oldWeapon = weaponProfile;
        int oldBulletCount = bulletCount;
        weaponProfile = weaponDrop.weaponProfile;
        bulletCount = weaponDrop.bulletCount;
        weaponDrop.UpdateInfoAndVisual(oldWeapon, oldBulletCount);
    }

    //load components logic
    //#--------------------------------------------------
    protected override void LoadComponents()
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
