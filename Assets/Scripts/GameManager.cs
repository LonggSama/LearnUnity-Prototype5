using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public List<GameObject> Targets;

    public TextMeshProUGUI ScoreText;

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
    }

    IEnumerator SpawnTarget()
    {
        while (true)
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
}
