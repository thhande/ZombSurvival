using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterSelectBtn : MonoBehaviour
{
    [SerializeField] Button charSelect;
    [SerializeField] CharacterData charData;
    [SerializeField] Image charIcon;
    void Start()
    {
        LoadComponents();
        charIcon.sprite = charData.icon;
        charSelect.onClick.AddListener(OnClickSelect);

    }

    public void OnClickSelect()
    {
        GameManager.instance.SetPlayer(this.charData.characterPrefab);
    }

    private void LoadComponents()
    {
        if (charSelect == null) charSelect = GetComponent<Button>();
        if (charIcon == null) charIcon = transform.Find("Image").GetComponent<Image>();
    }

    private void OnValidate()
    {
        LoadComponents();
    }


}
