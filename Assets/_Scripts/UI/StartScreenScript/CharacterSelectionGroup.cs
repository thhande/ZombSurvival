using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterSelectionGroup : SelectionGroup
{
    [SerializeField] private SelectionGroup selectionGroup;
    [SerializeField] private CharacterSelectBtn selectBtn;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        if (selectionGroup == null) selectionGroup = GetComponent<SelectionGroup>();
    }

    private void GenerateCharSelectButtons(CharacterData charData)
    {
        CharacterSelectBtn btn = Instantiate(selectBtn, selectionGroup.transform);
        btn.SetCharData(charData);
    }
}
