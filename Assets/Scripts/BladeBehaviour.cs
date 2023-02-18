using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BladeBehaviour : MonoBehaviour
{
    [SerializeField] GameObject _bladeTrailPrefab;

    [SerializeField] float _minCuttingVelocity;

    [SerializeField] float _minTimeHold;

    GameObject _currentBladeTrail;

    BoxCollider _bladeCollider;

    Camera _cam;

    Rigidbody _rb;

    Vector2 _previousPos;

    float velocity;

    float _timeStamp;

    float _timeHold;

    public bool IsCutting;

    // Start is called before the first frame update
    void Start()
    {
        _rb = this.GetComponent<Rigidbody>();

        _cam = Camera.main;

        _bladeCollider = GetComponent<BoxCollider>();
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.Instance.IsGameActive && !GameManager.Instance.IsPaused)
        {
            if (Input.GetMouseButtonDown(0))
            {
                _timeStamp = Time.time;
                StartCutting();
            }

            if (Input.GetMouseButtonUp(0) && IsCutting)
            {
                StopCutting();
            }

            if (IsCutting)
            {
                UpdateCut();
            }
        }
    }

    void UpdateCut()
    {
        Vector2 newPos = _cam.ScreenToWorldPoint(Input.mousePosition);

        this.transform.position = newPos ;

        velocity = (newPos - _previousPos).magnitude * Time.deltaTime;

        //_previousPos = newPos;

        //if (velocity > _minCuttingVelocity)
        //{
        //    _bladeCollider.enabled = true;
        //}
        //else
        //    _bladeCollider.enabled = false;

        _timeHold = Time.time - _timeStamp;

        if (_timeHold > _minTimeHold || velocity > _minCuttingVelocity)
        {
            _bladeCollider.enabled = true;
        }
        else
            _bladeCollider.enabled = false;
    }

    void StartCutting()
    {
        IsCutting = true;

        _currentBladeTrail = Instantiate(_bladeTrailPrefab, transform);

        _previousPos = _cam.ScreenToWorldPoint(Input.mousePosition);

        
    }

    void StopCutting()
    {
        IsCutting = false;

        _currentBladeTrail.transform.SetParent(null);

        Destroy(_currentBladeTrail, 0.5f);

        _bladeCollider.enabled = false;
    }
}
