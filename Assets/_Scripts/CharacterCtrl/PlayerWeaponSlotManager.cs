using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

public class PlayerWeaponSlotManager : MonoBehaviour
{
    [SerializeField] List<PlayerWeaponSlots> weaponSlots = new List<PlayerWeaponSlots>();
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
        weaponSlots = new List<PlayerWeaponSlots>(GetComponentsInChildren<PlayerWeaponSlots>());
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("WeaponDrop"))
        {
            weaponToPickup = collision.GetComponent<WeaponDropContainer>();
            UIInputManager.instance.ShowWeaponChangeButton();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("WeaponDrop"))
        {

            weaponToPickup = null;
            UIInputManager.instance.HideWeaponChangeButton();
        }
    }

    private void HandleWeaponPickupInput()
    {

        if (weaponToPickup == null) return;
        int index = -1;
        if (InputManager.instance.ChangeWeaponInSlotOne())
        {
            index = 0;
        }
        else if (InputManager.instance.ChangeWeaponInSlotTwo())
        {
            index = 1;
        }
        if (index != -1) weaponSlots[index].AddNewWeapon(weaponToPickup);

    }
}
