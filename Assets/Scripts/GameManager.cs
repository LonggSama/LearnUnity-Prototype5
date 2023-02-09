using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public List<GameObject> Targets;

    private float _spawnRate = 1.0f;

    private void Start()
    {
        StartCoroutine(SpawnTarget());
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
}
