using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    [SerializeField] protected int damage = 5;
    private Rigidbody2D _rb;
    private Vector3 _prevPos;
    protected Collider2D _col;
    protected GameObject _obj;
    
    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _col = GetComponent<Collider2D>();
    }

    private void Start()
    {
        Destroy(gameObject, 10f);
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

    protected void Land()
    {
        _rb.bodyType = RigidbodyType2D.Static;
        _col.enabled = false;
        Destroy(gameObject, 5f);
    }

    private void BaseMoveByAir()
    {
        if (_rb.velocity == Vector2.zero)
            return;
        transform.rotation = Quaternion.LookRotation(Vector3.forward, (transform.position - _prevPos)*Time. deltaTime);
        _prevPos = transform.position;
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        _obj = col.gameObject; 
        if (_obj.name == "Ground")
        {
            Land();
        }
        if (_obj.tag == "Enemy")
        {
            if (_col.enabled == false) return;
            _col.enabled = false;
            _obj.GetComponent<BasicEnemy>().TakeDamage(damage);
            Destroy(gameObject);
        }
    }
}
