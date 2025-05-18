using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    [SerializeField] GameObject charPrefab;
    [SerializeField] GameObject player;


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
        ResetValue();
        if (charPrefab != null) Instantiate(charPrefab);
    }

    public void ResetValue()
    {
        wave = 0;
        score = 0;
    }

    public void StartNewGame()
    {
        if (charPrefab == null) return;
        isGameActive = true;
        Instantiate(charPrefab);
        SceneManager.LoadScene(1);

    }
    public void GameOver()
    {
        isGameActive = false;
        ResetValue();
        OnGameOver();
    }


    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.buildIndex == 1 && charPrefab != null)
        {
            Debug.Log("✅ Scene đã load xong, giờ mới spawn player");
            player = Instantiate(charPrefab, Vector3.zero, Quaternion.identity);
            var healthbar = GameObject.FindAnyObjectByType<Healthbar>();
            healthbar.SetPlayer(player.transform.GetComponentInChildren<PlayerDamageReceiver>());
        }
    }
    public void SetPlayer(GameObject playerPrefab)
    {
        charPrefab = playerPrefab;
    }



    private void Awake()
    {
        if (instance != null) Destroy(gameObject);
        else
        {
            instance = this;
            LoadComponent();
        }
        DontDestroyOnLoad(gameObject);
    }


    private void LoadComponent()
    {
        return;
    }
}
