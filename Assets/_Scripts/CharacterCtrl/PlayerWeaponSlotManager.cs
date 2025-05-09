using System.Collections;
using System.Collections.Generic;
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
        Start();
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
        if (collision.CompareTag("WeaponDrop")) weaponToPickup = collision.GetComponent<WeaponDropContainer>();
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("WeaponDrop")) weaponToPickup = null;
    }

    private void HandleWeaponPickupInput()
    {
        if (weaponToPickup == null) return;
        if (InputManager.instance.ChangeWeaponInSlotOne())
        {
            weaponSlots[0].AddNewWeapon(weaponToPickup);
        }
        else if (InputManager.instance.ChangeWeaponInSlotTwo())
        {
            weaponSlots[1].AddNewWeapon(weaponToPickup);
        }
    }
}
