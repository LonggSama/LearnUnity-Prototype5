using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DifficultyButton : MonoBehaviour
{
    [SerializeField] int _difficulty;

    [SerializeField] private AudioClip _clip;

    private Button _button;

    private bool _clickButton;

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
        if (!_clickButton)
        {
            Debug.Log(_button.gameObject.name + " was clicked!");

            GameManager.Instance.StartGame(_difficulty);

            SoundManager.Instance.PlaySound(_clip);

            _clickButton = true;
        }
        
    }
}
