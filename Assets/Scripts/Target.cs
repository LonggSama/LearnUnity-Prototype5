using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    public int _pointValue;
    public ParticleSystem ExplosionParticle;

    private int _minForce = 12;
    private int _maxForce = 16;
    private int _rangeTorque = 10;
    private float _rangeX = 4f;
    private float _rangeY = -3f;

    private Rigidbody _targetRb;

    private void Awake()
    {
        _targetRb = GetComponent<Rigidbody>();
    }

    // Start is called before the first frame update
    void Start()
    {
        TargetAppear();
    }

    void TargetAppear()
    {
        // Add Force to Target
        _targetRb.AddForce(RandomForce(), ForceMode.Impulse);
        // Add Torque to 3 dimension
        _targetRb.AddTorque(RandomTorque(), RandomTorque(), RandomTorque(), ForceMode.Impulse);
        // First Appear
        transform.position = RandomAppearePos();
    }

    Vector3 RandomForce()
    {
        return Vector3.up * Random.Range(_minForce, _maxForce);
    }

    Vector2 RandomAppearePos()
    {
        return new Vector2(Random.Range(-_rangeX, _rangeX), _rangeY);
    }

    float RandomTorque()
    {
        return Random.Range(-_rangeTorque, _rangeTorque);
    }

    private void OnMouseDown()
    {
        Destroy(gameObject);
        Instantiate(ExplosionParticle, transform.position, ExplosionParticle.transform.rotation);
        GameManager.Instance.UpdateScore(_pointValue);
        if (gameObject.CompareTag("Bad"))
        {
            GameManager.Instance.UpdateLive(-1);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Sensor") && !gameObject.CompareTag("Bad"))
        {
            Destroy(gameObject);
            GameManager.Instance.UpdateLive(-1);
        }
    }

}
