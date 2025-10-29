using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Linq;

public class SelectionGroup : MMono
{
    [SerializeField] private Color normalColor = Color.white;
    [SerializeField] private Color selectedColor = Color.green;
    [SerializeField] private List<Button> buttons;

    private int selectedIndex = -1;

    protected virtual void Start()
    {
        SetSelectionOnClick();
    }

    private void OnButtonClicked(int index)
    {
        if (selectedIndex == index) return;

        if (selectedIndex != -1)
            SetButtonColor(buttons[selectedIndex], normalColor);

        selectedIndex = index;
        SetButtonColor(buttons[index], selectedColor);

        Debug.Log($"Selected option: {buttons[index].name}");
    }

    private void SetButtonColor(Button button, Color color)
    {
        var colors = button.colors;
        colors.normalColor = color;
        colors.selectedColor = color;
        button.colors = colors;
    }

    public int GetSelectedIndex() => selectedIndex;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        LoadSelectionButton();
    }

    private void LoadSelectionButton()
    {
        buttons = GetComponentsInChildren<Button>().ToList<Button>();
    }

    private void SetSelectionOnClick()
    {
        for (int i = 0; i < buttons.Count; i++)
        {
            int index = i;
            buttons[i].onClick.AddListener(() => OnButtonClicked(index));
            SetButtonColor(buttons[i], normalColor);
        }
    }
}
