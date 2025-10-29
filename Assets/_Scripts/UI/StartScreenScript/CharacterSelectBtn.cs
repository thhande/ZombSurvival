using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterSelectBtn : MMono
{
    [SerializeField] Button charSelect;
    [SerializeField] CharacterData charData;
    [SerializeField] Image charIcon;
    void Start()
    {
        LoadComponents();
        UpdateButtonInfo();
        charSelect.onClick.AddListener(OnClickSelect);

    }

    public void SetCharData(CharacterData characterData)
    {
        charData = characterData;
        UpdateButtonInfo();

    }

    private void UpdateButtonInfo()
    {
        if (charData == null) return;
        charIcon.sprite = charData.icon;
    }


    public void OnClickSelect()
    {
        GameManager.Instance.SetPlayer(this.charData.characterPrefab);
    }

    protected override void LoadComponents()
    {
        if (charSelect == null) charSelect = GetComponent<Button>();
        if (charIcon == null) charIcon = transform.Find("Image").GetComponent<Image>();
    }

}
