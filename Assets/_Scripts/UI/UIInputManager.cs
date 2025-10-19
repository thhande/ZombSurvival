using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIInputManager : Singleton<UIInputManager>
{


    [SerializeField] private GameObject weaponChangeButton;
    [SerializeField] private MireFixedJoystick movementJoystick;
    [SerializeField] private MireFixedJoystick attackJoystick;
    [SerializeField] List<Image> weaponSlotImages = new List<Image>();
    [SerializeField] List<TextMeshProUGUI> weaponSlotBulletCount = new List<TextMeshProUGUI>();

    public MireFixedJoystick MovementJoystick => movementJoystick;
    public MireFixedJoystick AttackJoystick => attackJoystick;

    //input button
    [SerializeField] private Button changeButtonOne;
    [SerializeField] private Button changeButtonTwo;
    [SerializeField] private Button switchButtonOne;
    [SerializeField] private Button switchButtonTwo;
    [SerializeField] private Button meleeAttackButton;


    private void Start()
    {
        weaponChangeButton.gameObject.SetActive(false);
        AssginInputButton();
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

    protected override void LoadComponents()
    {
        if (weaponChangeButton == null) weaponChangeButton = transform.Find("WeaponChange").gameObject;
        if (movementJoystick == null) movementJoystick = transform.Find("MovementJoystick").GetComponent<MireFixedJoystick>();
        if (attackJoystick == null) attackJoystick = transform.Find("ShootJoystick").GetComponent<MireFixedJoystick>();
        if (weaponSlotImages.Count == 0) LoadSlotImages();
        if (weaponSlotBulletCount.Count == 0) LoadSlotBulletCount();
        LoadUIButton(ref meleeAttackButton, "MeleeAttack");
        LoadSlotsButton(ref changeButtonOne, "WeaponChange", "Change1");
        LoadSlotsButton(ref changeButtonTwo, "WeaponChange", "Change2");
        LoadSlotsButton(ref switchButtonOne, "WeaponSwitch", "Slot1");
        LoadSlotsButton(ref switchButtonTwo, "WeaponSwitch", "Slot2");

    }
    private void LoadSlotImages()
    {
        weaponSlotImages.Clear();

        Transform weaponSwitch = transform.Find("WeaponSwitch");
        if (weaponSwitch == null) return; //" cannot find WeaponSwitch"           

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
    private void LoadUIButton(ref Button button, string objectName) //load the input UI button
    {
        if (button != null) return;
        button = transform.Find(objectName).GetComponent<Button>();
    }
    private void LoadSlotsButton(ref Button button, string objectName, string slotName)//load the UI buttons have impact on weapon slots
    {
        if (button != null) return;
        button = transform.Find(objectName).Find(slotName).GetComponent<Button>();
    }

    private void AssginInputButton()
    {
        InputManager input = InputManager.Instance;
        changeButtonOne.onClick.AddListener(() => input.UIChangeWeaponInSlotOne());
        changeButtonTwo.onClick.AddListener(() => input.UIChangeWeaponInSlotTwo());
        switchButtonOne.onClick.AddListener(() => input.UISwitchToWeaponSlotOne());
        switchButtonTwo.onClick.AddListener(() => input.UISwitchToWeaponSlotTwo());
        meleeAttackButton.onClick.AddListener(() => input.UIMeleeAttackInput());
    }



}




