using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI scoreText;
    [SerializeField] TextMeshProUGUI waveCount;
    [SerializeField] TextMeshProUGUI timeCount;
    [SerializeField] TextMeshProUGUI highScoreText;

    [SerializeField] UIInputManager uIInput;


    [SerializeField] GameObject gameOverScreen;

    [SerializeField] List<Image> buffIcons;
    [SerializeField] PlayerBuffs playerBuffs;



    private void Start()
    {
        LoadComponent();
        GameManager.Instance.OnScoreChange += UpdateScoreText;
        GameManager.Instance.OnWaveChange += UpdateWaveCount;
        GameManager.Instance.OnGameOver += GameOverUIUpdate;
        GameManager.Instance.OnTimePasses += UpdateTimePasses;
        GameManager.Instance.OnHScoreChange += UpdateHScoreText;
        playerBuffs.OnBuffChanged += UpDateBuffIcons;
        UpdateHScoreText();
        UpdateScoreText();
    }

    public void UpdateScoreText()
    {
        scoreText.text = GameManager.Instance.score.ToString();
    }

    public void UpdateWaveCount()
    {
        waveCount.text = "wave :" + GameManager.Instance.wave.ToString();
    }

    public void UpdateTimePasses()
    {
        timeCount.text = GameManager.Instance.playTime.ToString();
    }


    private void UpDateBuffIcons()
    {
        for (int i = 0; i < buffIcons.Count; i++)
        {

            if (i < playerBuffs.activeBuffs.Count)
            {
                buffIcons[i].sprite = playerBuffs.activeBuffs[i].buffData.buffIcon;
                buffIcons[i].gameObject.SetActive(true);
            }
            else
            {
                buffIcons[i].sprite = null;
                buffIcons[i].gameObject.SetActive(false);
            }
        }
    }

    private void UpdateHScoreText()
    {
        highScoreText.text = "Hi:" + GameManager.Instance.highScore.ToString();
    }

    private void OnDestroy()
    {
        if (GameManager.Instance != null)
        {
            GameManager.Instance.OnScoreChange -= UpdateScoreText;
            GameManager.Instance.OnWaveChange -= UpdateWaveCount;
            GameManager.Instance.OnGameOver -= GameOverUIUpdate;
            GameManager.Instance.OnTimePasses -= UpdateTimePasses;
            GameManager.Instance.OnHScoreChange -= UpdateHScoreText;
            if (playerBuffs != null) playerBuffs.OnBuffChanged -= UpDateBuffIcons;
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
        if (highScoreText == null) highScoreText = transform.Find("HighScore").GetComponent<TextMeshProUGUI>();
        if (gameOverScreen == null)
        {
            gameOverScreen = transform.Find("GameOverScreen").gameObject;
            gameOverScreen.SetActive(false);

        }
        // if (restartButton == null) restartButton = gameOverScreen.GetComponentInChildren<Button>();
        if (uIInput == null) uIInput = transform.GetComponentInChildren<UIInputManager>();

        playerBuffs = GameObject.FindAnyObjectByType<PlayerBuffs>();

        LoadBuffIcon();

    }

    private void LoadBuffIcon()
    {
        if (buffIcons.Count > 0) return;
        buffIcons = transform.Find("BuffIcons").GetComponentsInChildren<Image>().ToList<Image>();
        foreach (Image icon in buffIcons)
        {
            icon.gameObject.SetActive(false);
        }
    }


#if UNITY_EDITOR
    private void OnValidate()
    {
        LoadComponent();
    }
#endif
}
