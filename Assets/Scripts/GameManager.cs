using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public List<GameObject> Targets;

    public TextMeshProUGUI ScoreText;
    public TextMeshProUGUI LiveText;
    public TextMeshProUGUI GameOverText;

    public bool IsGameOver;

    [SerializeField] private float _easyRate;
    [SerializeField] private float _hardRate;
    [SerializeField] private int _live;

    private float _timeUpdate = 1.5f;
    private int _score;

    private int _spawnRate;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(this);
        }
        else if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this);
        }
    }

    private void Start()
    {
        _score = 0;
        StartCoroutine(SpawnTarget());
        UpdateScore(0);
        UpdateLive(0);
    }

    private void Update()
    {
        if (IsGameOver)
        {
            GameOverText.gameObject.SetActive(true);
        }
    }

    IEnumerator SpawnTarget()
    {
        while (!IsGameOver)
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
        _score += scoreToAdd;
        if (_score < 0)
        {
            _score = 0;
        }
        ScoreText.text = "Score: " + _score;
    }

    public void UpdateLive(int liveToAdd)
    {
        _live += liveToAdd;
        if (_live < 1)
        {
            _live = 0;
            IsGameOver = true;
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
}
