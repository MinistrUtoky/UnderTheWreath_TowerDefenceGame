using System;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    private Rigidbody2D _rb;
    private Vector3 _prevPos;
    private Collider2D _col;
    
    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _col = GetComponent<Collider2D>();
    }

    private void Start()
    {
        _prevPos = transform.position;
    }
    
    private void FixedUpdate()
    {
        BaseMoveByAir();
    }

    public void Throw(Vector2 forceVector)
    {
        _rb.AddForce(forceVector, ForceMode2D.Impulse);
    }

    private void Land()
    {
        _rb.bodyType = RigidbodyType2D.Static;
        _col.enabled = false;
    }

    private void BaseMoveByAir()
    {
        if (_rb.velocity == Vector2.zero)
            return;
        transform.rotation = Quaternion.LookRotation(Vector3.forward, (transform.position - _prevPos)*Time. deltaTime);
        _prevPos = transform.position;
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.name == "Ground")
        {
            Land();
        }
    }
}
