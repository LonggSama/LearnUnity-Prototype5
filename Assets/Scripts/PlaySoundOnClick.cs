using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlaySoundOnClick : MonoBehaviour
{
    [SerializeField] private AudioClip _clip;

    private Button _button;

    private bool _clickButton;

    // Start is called before the first frame update
    void Start()
    {
        _button = GetComponent<Button>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!_clickButton)
        {
            _button.onClick.AddListener(PlayOnClick);

            _clickButton = true;
        }
        
    }

    void PlayOnClick()
    {
        SoundManager.Instance.PlaySound(_clip);
    }

    
}
