using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class UIInputManager : MonoBehaviour
{
    public static UIInputManager instance;

    [SerializeField] private GameObject weaponChangeButton;
    [SerializeField] public MireFixedJoystick joystick;
    [SerializeField] List<Image> weaponSlotImages = new List<Image>();
    [SerializeField] List<TextMeshProUGUI> weaponSlotBulletCount = new List<TextMeshProUGUI>();

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        instance = this;
    }
    private void Start()
    {
        LoadComponents();
        weaponChangeButton.gameObject.SetActive(false);
    }


    public void ShowWeaponChangeButton()
    {
        weaponChangeButton.gameObject.SetActive(true);
    }
    public void HideWeaponChangeButton()
    {
        if (weaponChangeButton != null) weaponChangeButton.gameObject.SetActive(false);
    }


    public void OnWeaponRemoved(int index)
    {
        if (index < 0 || index >= weaponSlotImages.Count) return;
        weaponSlotImages[index].sprite = null;
        weaponSlotImages[index].gameObject.SetActive(false);
        UpdateWeaponBulletCount(index, 0);
    }


    public void SetWeaponSlotSprite(int index, Sprite sprite)
    {
        if (index < 0 || index >= weaponSlotImages.Count) return;
        weaponSlotImages[index].sprite = sprite;
        weaponSlotImages[index].gameObject.SetActive(true);

    }

    public void UpdateWeaponBulletCount(int index, int bulletCount)
    {
        if (index < 0 || index >= weaponSlotBulletCount.Count) return;
        weaponSlotBulletCount[index].text = bulletCount.ToString();
        weaponSlotBulletCount[index].gameObject.SetActive(true);
    }

    private void OnValidate()
    {
#if UNITY_EDITOR
        LoadComponents();
#endif
    }

    private void LoadComponents()
    {
        if (weaponChangeButton == null) weaponChangeButton = transform.Find("WeaponChange").gameObject;
        if (joystick == null) joystick = transform.Find("Joystick").GetComponent<MireFixedJoystick>();
        if (weaponSlotImages.Count == 0) LoadSlotImages();
        if (weaponSlotBulletCount.Count == 0) LoadSlotBulletCount();

    }
    private void LoadSlotImages()
    {
        weaponSlotImages.Clear();

        Transform weaponSwitch = transform.Find("WeaponSwitch");
        if (weaponSwitch == null)
        {
            Debug.LogWarning(" Không tìm thấy WeaponSwitch");
            return;
        }

        foreach (Transform slot in weaponSwitch) // Slot1, Slot2
        {
            Transform childImage = slot.Find("Image");
            if (childImage != null)
            {
                Image img = childImage.GetComponent<Image>();
                if (img != null)
                {

                    weaponSlotImages.Add(img);
                    img.gameObject.SetActive(false);

                }
            }
        }

        Debug.Log($"✅ Loaded {weaponSlotImages.Count} weapon slot images.");
    }
    private void LoadSlotBulletCount()
    {
        weaponSlotBulletCount.Clear();

        Transform weaponSwitch = transform.Find("WeaponSwitch");
        if (weaponSwitch == null)
        {
            Debug.LogWarning("cannnot find WeaponSwitch");
            return;
        }

        foreach (Transform slot in weaponSwitch) // Slot1, Slot2
        {
            TextMeshProUGUI bulletCount = slot.GetComponentInChildren<TextMeshProUGUI>();
            if (bulletCount != null)
            {
                weaponSlotBulletCount.Add(bulletCount);
                bulletCount.gameObject.SetActive(false);
            }


        }
    }


}




