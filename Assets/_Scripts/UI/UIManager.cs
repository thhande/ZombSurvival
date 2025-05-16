using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI scoreText;
    [SerializeField] TextMeshProUGUI waveCount;
    [SerializeField] GameObject gameOverScreen;
    [SerializeField] UIInputManager uIInput;

    private void Start()
    {
        LoadComponent();
        GameManager.instance.OnScoreChange += UpdateScoreText;
        GameManager.instance.OnWaveChange += UpdateWaveCount;
        GameManager.instance.OnGameOver += GameOverUIUpdate;

        UpdateScoreText();
    }

    public void UpdateScoreText()
    {
        scoreText.text = GameManager.instance.score.ToString();
    }

    public void UpdateWaveCount()
    {
        waveCount.text = "wave :" + GameManager.instance.wave.ToString();
    }

    public void GameOverUIUpdate()
    {
        gameOverScreen.SetActive(true);
        uIInput.gameObject.SetActive(false);
    }


    private void LoadComponent()
    {
        if (scoreText == null) scoreText = transform.Find("Score").GetComponentInChildren<TextMeshProUGUI>();
        if (waveCount == null) waveCount = transform.Find("WaveCount").GetComponent<TextMeshProUGUI>();
        if (gameOverScreen == null)
        {
            gameOverScreen = transform.Find("GameOverScreen").gameObject;
            gameOverScreen.SetActive(false);

        }
        if (uIInput == null) uIInput = transform.GetComponentInChildren<UIInputManager>();
    }


    private void OnValidate()
    {
        LoadComponent();
    }
}
