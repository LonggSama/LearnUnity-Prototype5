using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] private float _easyRate;

    [SerializeField] private float _hardRate;

    [SerializeField] private int _live;

    [SerializeField] private Button _restartButton;

    [SerializeField] private GameObject _titleScreen;

    [SerializeField] private GameObject _pauseText;

    [SerializeField] private GameObject _pausePanel;

    [SerializeField] private GameObject _blade;

    public static GameManager Instance;

    public List<GameObject> Targets;

    public TextMeshProUGUI ScoreText;

    public TextMeshProUGUI LiveText;

    public TextMeshProUGUI GameOverText;

    public bool IsGameActive;

    private float _timeUpdate;

    private float _difficulty;

    private int _score;

    private int _spawnRate;

    public bool IsPaused;

    public bool _firstWave = false;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        StartCoroutine(FirstWave());

        StartCoroutine(AfterFirstWave());
    }

    private void Update()
    {
        PauseGame();
    }
    IEnumerator FirstWave()
    {
        yield return new WaitUntil(() => IsGameActive);

        int targetIndex = Random.Range(0, Targets.Count);

        Instantiate(Targets[targetIndex]);

        _firstWave = true;
    }

    IEnumerator AfterFirstWave()
    {
        yield return new WaitUntil(() => _firstWave);
        StartCoroutine(SpawnTarget());
    }

    IEnumerator SpawnTarget()
    {
        while (IsGameActive)
        {
            TimeUpdate();

            yield return new WaitForSeconds(_timeUpdate/_difficulty);

            UpdateSpawnRate();

            for (int i = 0; i < _spawnRate; i++)
            {
                int targetIndex = Random.Range(0, Targets.Count);

                Instantiate(Targets[targetIndex]);
            }
        }
    }

    //void SpawnTarget()
    //{
    //    for (int i = 0; i < _spawnRate; i++)
    //    {
    //        int targetIndex = Random.Range(0, Targets.Count);
    //        Instantiate(Targets[targetIndex]);
    //    }
    //}

    public void UpdateScore(int scoreToAdd)
    {
        if (IsGameActive)
        {
            _score += scoreToAdd;

            if (_score < 0)
            {
                _score = 0;
            }

            ScoreText.text = "Score: " + _score;
        }
    }

    public void UpdateLive(int liveToAdd)
    {
        _live += liveToAdd;

        if (_live < 1)
        {
            _live = 0;

            GameOver();
        }

        LiveText.text = "Lives: " + _live;
    }

    float UpdateSpawnRate()
    {
        if (Random.value <= _easyRate)
        {
            _spawnRate = 1;
        }
        if (Random.value > _easyRate && Random.value < _hardRate)
        {
            _spawnRate = 2;
        }
        if (Random.value >= _hardRate)
        {
            _spawnRate = 3;
        }
        return _spawnRate;
    }

    float TimeUpdate()
    {
        if (Random.value <= _easyRate)
        {
            _timeUpdate = 2f;
        }
        if (Random.value > _easyRate && Random.value < _hardRate)
        {
            _timeUpdate = 1.5f;
        }
        if (Random.value >= _hardRate)
        {
            _timeUpdate = 1f;
        }

        return _timeUpdate;
    }

    void GameOver()
    {
        IsGameActive = false;

        GameOverText.gameObject.SetActive(true);

        _restartButton.gameObject.SetActive(true);
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void StartGame(int difficulty)
    {
        IsGameActive = true;

        _score = 0;

        _difficulty = difficulty;

        UpdateScore(0);

        UpdateLive(0);

        _titleScreen.gameObject.SetActive(false);
    }

    public void PauseGame()
    {
        if (IsGameActive)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                IsPaused = !IsPaused;

                if (IsPaused)
                {
                    Time.timeScale = 0;

                    _pausePanel.SetActive(true);

                    _pauseText.SetActive(true);
                }
                else
                {
                    Time.timeScale = 1;

                    _pausePanel.SetActive(false);

                    _pauseText.SetActive(false);
                }
            }
        }
    }
}
