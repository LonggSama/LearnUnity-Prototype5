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

    [SerializeField] private int _live;
    private int _score;

    private float _spawnRate = 1.0f;

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
            yield return new WaitForSeconds(_spawnRate);
            int targetIndex = Random.Range(0, Targets.Count);
            Instantiate(Targets[targetIndex]);
        }
    }

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
}
