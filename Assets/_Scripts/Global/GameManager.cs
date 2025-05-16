using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    [SerializeField] UIManager uiManager;
    public int score = 0;
    public int highScore;


    public bool isGameActive;
    public int wave = 1;


    public event System.Action OnScoreChange;
    public event System.Action OnWaveChange;
    public event System.Action OnGameOver;

    public void AddScore(int amount)
    {
        score += amount;
        OnScoreChange();
    }

    public void EnemyWaveUpdate()
    {
        wave++;
        OnWaveChange();
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void GameOver()
    {
        OnGameOver();
    }


    private void Awake()
    {
        if (instance != null) Destroy(gameObject);
        else
        {
            instance = this;
            LoadComponent();
        }
    }


    private void LoadComponent()
    {
        if (uiManager == null) uiManager = GameObject.FindObjectOfType<UIManager>();
    }
}
