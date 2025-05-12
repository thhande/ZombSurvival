using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class UIInputManager : MonoBehaviour
{
    public static UIInputManager instance;

    [SerializeField] private GameObject weaponChangeButton;
    [SerializeField] public MireFixedJoystick joystick;
    [SerializeField] List<Image> weaponSlotImages = new List<Image>();

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
    public void OnWeaponRemoved(int index)
    {
        if (index < 0 || index >= weaponSlotImages.Count) return;
        weaponSlotImages[index].sprite = null;
        weaponSlotImages[index].gameObject.SetActive(false);
    }

    private void Start()
    {
        LoadComponents();
        weaponChangeButton.gameObject.SetActive(false);
    }

    public void SetWeaponSlotSprite(int index, Sprite sprite)
    {
        if (index < 0 || index >= weaponSlotImages.Count) return;
        weaponSlotImages[index].sprite = sprite;
        weaponSlotImages[index].gameObject.SetActive(true);

    }

    private void LoadComponents()
    {
        if (weaponChangeButton == null) weaponChangeButton = transform.Find("WeaponChange").gameObject;
        if (joystick == null) joystick = transform.Find("Joystick").GetComponent<MireFixedJoystick>();
        if (weaponSlotImages.Count == 0) LoadSlotImages();

    }
    private void LoadSlotImages()
    {
        weaponSlotImages.Clear();

        Transform weaponSwitch = transform.Find("WeaponSwitch");
        if (weaponSwitch == null)
        {
            Debug.LogWarning("❌ Không tìm thấy WeaponSwitch");
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



}
