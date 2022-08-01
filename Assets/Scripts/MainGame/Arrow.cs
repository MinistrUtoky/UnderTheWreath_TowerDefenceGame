using System;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    private GameObject _tip;
    private Rigidbody2D _rb;
    private Collider2D _tipCol;
    private GameObject _arrowBase;
    private float _baseTipDistance;
    
    private void Awake()
    {
        _tip = transform.Find("Tip").gameObject;
        _rb = GetComponent<Rigidbody2D>();
        _tipCol = _tip.GetComponent<Collider2D>();
        _arrowBase = transform.Find("Base").gameObject;
    }

    private void Start()
    {
        _baseTipDistance = (_tip.transform.position - _arrowBase.transform.position).magnitude;
    }
    
    private void Update()
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
        _rb.velocity = Vector2.zero;
    }

    private void BaseMoveByAir()
    {
        if (_rb.velocity == Vector2.zero)
            return;
        var baseRotationDirection = -_rb.velocity.normalized;
        _arrowBase.transform.position = _tip.transform.position + (Vector3)(baseRotationDirection * _baseTipDistance);
        transform.rotation =
            Quaternion.LookRotation(Vector3.forward, _tip.transform.position - _arrowBase.transform.position);
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Ground")
        {
            Land();
        }
    }
}
