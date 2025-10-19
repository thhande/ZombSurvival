using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    [SerializeField] GameObject charPrefab;
    [SerializeField] GameObject player;

    public GameObject CurrentPlayer => player;
    public int score = 0;
    public int highScore;

    public float playTime;
    private float defaultTimeLeft = 60;


    public bool isGameActive;
    public int wave = 1;

    Coroutine timeCoroutine;

    public event System.Action OnScoreChange;
    public event System.Action OnWaveChange;
    public event System.Action OnGameOver;
    public event System.Action OnTimePasses;
    public event System.Action OnHScoreChange;

    public void AddScore(int amount)
    {
        score += amount;
        if (score > highScore)
        {
            highScore = score;
            OnHScoreChange();
        }
        playTime += Random.Range(2, 6);
        OnTimePasses();
        OnScoreChange();
    }

    public void EnemyWaveUpdate()
    {
        wave++;
        OnWaveChange?.Invoke();
    }

    public void Restart()
    {

        ResetValue();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);

        isGameActive = true;
    }

    public void ResetValue()
    {
        StopAllCoroutines();
        playTime = defaultTimeLeft;
        wave = 0;
        score = 0;
    }

    public void StartNewGame()
    {
        if (charPrefab == null) return;

        ResetValue();
        isGameActive = true;
        SceneManager.LoadScene(1);
    }


    public void GameOver()
    {
        SaveSystem.SaveHighScore(highScore);
        isGameActive = false;
        // StopAllCoroutines();
        ResetValue();
        if (player != null) Destroy(player);
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
            // Debug.Log("Scene đã load xong, giờ mới spawn player");
            player = Instantiate(charPrefab, Vector3.zero, Quaternion.identity);
            var healthbar = GameObject.FindAnyObjectByType<Healthbar>();
            healthbar.SetPlayer(player.transform.GetComponentInChildren<PlayerDamageReceiver>());
        }
        if (scene.buildIndex == 1 && isGameActive)
        {
            if (timeCoroutine != null) StopCoroutine(timeCoroutine);
            timeCoroutine = StartCoroutine(TimeReduce());
        }

    }

    public void SetPlayer(GameObject playerPrefab)
    {
        charPrefab = playerPrefab;
    }

    IEnumerator TimeReduce()
    {
        while (isGameActive)
        {
            yield return new WaitForSeconds(1);
            playTime -= 1;
            OnTimePasses();
            if (playTime <= 0)
            {
                GameOver();
            }
        }
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
        highScore = SaveSystem.Load().highScore;

    }
    // private void Start() // for test scene
    // {

    //     isGameActive = true;
    //     ResetValue();
    //     StartCoroutine(TimeReduce());
    // }


    private void LoadComponent()
    {
        return;
    }
}
