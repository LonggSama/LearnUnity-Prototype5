using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DifficultyButton : MonoBehaviour
{
    [SerializeField] int _difficulty;

    private Button _button;

    // Start is called before the first frame update
    void Awake()
    {
        _button = GetComponent<Button>();
    }

    // Update is called once per frame
    void Update()
    {
        _button.onClick.AddListener(SetDifficulty);
    }

    void SetDifficulty()
    {
        Debug.Log(_button.gameObject.name + " was clicked!");

        GameManager.Instance.StartGame(_difficulty);
    }
}
