using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

public class PlayerWeaponSlotManager : MonoBehaviour
{
    [SerializeField] List<PlayerWeaponSlot> weaponSlots = new List<PlayerWeaponSlot>();
    [SerializeField] private WeaponDropContainer weaponToPickup;

    private void Update()
    {
        HandleWeaponPickupInput();
    }
    private void OnValidate()
    {
        LoadComponents();
    }

    private void Start()
    {
        LoadComponents();
    }

    private void LoadComponents()
    {
        weaponSlots = new List<PlayerWeaponSlot>(GetComponentsInChildren<PlayerWeaponSlot>());
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("WeaponDrop"))
        {
            weaponToPickup = collision.GetComponent<WeaponDropContainer>();
            UIInputManager.Instance.ShowWeaponChangeButton();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("WeaponDrop"))
        {

            weaponToPickup = null;
            UIInputManager.Instance.HideWeaponChangeButton();
        }
    }

    private void HandleWeaponPickupInput()
    {

        if (weaponToPickup == null) return;
        int index = NewSlotIndex();
        if (index != -1) weaponSlots[index].AddNewWeapon(weaponToPickup);

    }

    private int NewSlotIndex()
    {
        if (InputManager.Instance.ChangeWeaponInSlotOne() || weaponSlots[0].weaponProfile == null || weaponSlots[0].weaponProfile == weaponToPickup.weaponProfile)
        {
            return 0;
        }
        else if (InputManager.Instance.ChangeWeaponInSlotTwo() || weaponSlots[1].weaponProfile == null || weaponSlots[1].weaponProfile == weaponToPickup.weaponProfile)
        {
            return 1;
        }
        return -1;
    }
}
