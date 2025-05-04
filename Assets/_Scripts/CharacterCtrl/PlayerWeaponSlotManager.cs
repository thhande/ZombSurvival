using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeaponSlotManager : MonoBehaviour
{
    [SerializeField] List<PlayerWeaponSlots> weaponSlots = new List<PlayerWeaponSlots>();
    [SerializeField] private WeaponDrop weaponToPickup;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("WeaponDrop")) weaponToPickup = collision.GetComponent<WeaponDrop>();
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("WeaponDrop")) weaponToPickup = null;
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
}
