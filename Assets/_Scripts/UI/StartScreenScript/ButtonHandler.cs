using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public enum ButtonAction
{
    restartgame, startnewgame, totitle
}

public class ButtonHandler : MonoBehaviour
{
    [SerializeField] private Button button;
    [SerializeField] public ButtonAction action;

    private void Awake()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(OnClick);
    }
    private void Start()
    {
        button.onClick.AddListener(OnClick);
    }


    private void OnClick()
    {
        switch (action)
        {
            case ButtonAction.startnewgame:
                GameManager.Instance.StartNewGame();
                break;
            case ButtonAction.restartgame:
                GameManager.Instance.Restart();
                break;
            case ButtonAction.totitle:
                SceneManager.LoadScene(0);
                break;

        }

    }
}
