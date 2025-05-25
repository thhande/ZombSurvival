using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI scoreText;
    [SerializeField] TextMeshProUGUI waveCount;
    [SerializeField] TextMeshProUGUI timeCount;

    [SerializeField] UIInputManager uIInput;


    [SerializeField] GameObject gameOverScreen;
    // [SerializeField] Button restartButton;



    private void Start()
    {
        LoadComponent();
        GameManager.instance.OnScoreChange += UpdateScoreText;
        GameManager.instance.OnWaveChange += UpdateWaveCount;
        GameManager.instance.OnGameOver += GameOverUIUpdate;
        GameManager.instance.onTimePasses += UpdateTimePasses;

        // restartButton.onClick.AddListener(GameManager.instance.Restart);

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

    public void UpdateTimePasses()
    {
        timeCount.text = GameManager.instance.playTime.ToString();
    }

    private void OnDestroy()
    {
        if (GameManager.instance != null)
        {
            GameManager.instance.OnScoreChange -= UpdateScoreText;
            GameManager.instance.OnWaveChange -= UpdateWaveCount;
            GameManager.instance.OnGameOver -= GameOverUIUpdate;
            GameManager.instance.onTimePasses -= UpdateTimePasses;
        }
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
        if (timeCount == null) timeCount = transform.Find("TimeCount").GetComponent<TextMeshProUGUI>();
        if (gameOverScreen == null)
        {
            gameOverScreen = transform.Find("GameOverScreen").gameObject;
            gameOverScreen.SetActive(false);

        }
        // if (restartButton == null) restartButton = gameOverScreen.GetComponentInChildren<Button>();
        if (uIInput == null) uIInput = transform.GetComponentInChildren<UIInputManager>();
    }


#if UNITY_EDITOR
    private void OnValidate()
    {
        LoadComponent();
    }
#endif
}
