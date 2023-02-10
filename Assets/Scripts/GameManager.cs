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

    public static GameManager Instance;

    public List<GameObject> Targets;

    public TextMeshProUGUI ScoreText;

    public TextMeshProUGUI LiveText;

    public TextMeshProUGUI GameOverText;

    public bool IsGameActive;

    private float _timeUpdate = 1.5f;

    private int _score;

    private int _spawnRate;

    private bool _firstWave;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        IsGameActive = true;

        _score = 0;

        StartCoroutine(FirstWave());

        StartCoroutine(AfterFirstWave());

        UpdateScore(0);

        UpdateLive(0);
    }

    IEnumerator FirstWave()
    {
        if (IsGameActive)
        {
            yield return new WaitForSeconds(0.5f);
            int targetIndex = Random.Range(0, Targets.Count);
            Instantiate(Targets[targetIndex]);
            _firstWave = true;
        }
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
            yield return new WaitForSeconds(_timeUpdate);
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
        LiveText.text = "Live: " + _live;
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
}
