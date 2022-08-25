using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Builder_Script : MonoBehaviour
{
    [SerializeField] private float _speed = 2f;
    [SerializeField] private int _healPower = 3;
    private Rigidbody2D _rb;
    private Vector3 _currentVelocity;

    void Start()
    {
        _currentVelocity = new Vector3(-_speed, 0, 0);
        _rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        BuilderMovement();
    }

    private void BuilderMovement()
    {
        if (transform.position.x > 8.7) Destroy(gameObject);
        _rb.velocity = _currentVelocity;
    }

    public void Retreat()
    { 
        _currentVelocity = new Vector3(_speed * 2, 0, 0);
    }

    public void Stop()
    { 
        _currentVelocity = Vector2.zero;
    }

    public int GetHealing()
    {
        return _healPower; 
    }
}
