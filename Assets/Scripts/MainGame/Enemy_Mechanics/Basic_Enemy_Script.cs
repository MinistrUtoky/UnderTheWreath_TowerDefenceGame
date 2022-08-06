using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Basic_Enemy_Script : MonoBehaviour
{
    [SerializeField] private float _speed = 3.0f;
    [SerializeField] private int _hp = 10;
    [SerializeField] private int _damage = 5;
    [SerializeField] private float _damageBackLeap = 10f;
    private Vector3 _backLeapVector;
    private Rigidbody2D _rb;

    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _backLeapVector = new Vector3(-_damageBackLeap, _damageBackLeap, 0);
    }

    void Update()
    {
        EnemyMovement();
    }

    private void EnemyMovement() {
        if (_rb.velocity == Vector2.zero) return;
        if (_rb.velocity.x < 0) return;
        _rb.velocity = new Vector3(_speed, 0, 0);
    }

    public void TakeDamage(int damage)
    {
        _hp -= damage;
        if (_hp <= 0) Destroy(gameObject);
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.name == "Wall")
        {
            _rb.AddForce(_backLeapVector);
        }
        if (col.gameObject.name == "Ground")
        {
            _rb.velocity = new Vector3(_speed, 0, 0);
        }
    }

}
