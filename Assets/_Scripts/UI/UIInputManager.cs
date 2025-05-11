using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIInputManager : MonoBehaviour
{
    public static UIInputManager instance;

    private GameObject weaponChangeButton;

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        instance = this;
    }

    public void ShowWeaponChangeButton()
    {
        weaponChangeButton.gameObject.SetActive(true);
    }
    public void HideWeaponChangeButton()
    {
        weaponChangeButton.gameObject.SetActive(false);
    }

    private void OnValidate()
    {
        LoadComponents();
    }


    private void Start()
    {
        LoadComponents();
        weaponChangeButton.gameObject.SetActive(false);
    }

    private void LoadComponents()
    {
        weaponChangeButton = transform.Find("WeaponChange").gameObject;
    }


}
