using System;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    [SerializeField] private float rotationSpeed;
    
    private Rigidbody2D _rb;
    private Vector3 _prevPos;
    private Collider2D _col;
    private Vector3 _prevDirection;

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
        if (transform.position == _prevPos)
            return;
        var position = transform.position;
        Vector3 direction = Vector3.Lerp(_prevDirection, position - _prevPos, rotationSpeed * Time.fixedDeltaTime);
        transform.rotation = Quaternion.LookRotation(Vector3.forward, direction);
        _prevPos = position;
        _prevDirection = direction;
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.name == "Ground")
        {
            Land();
        }
    }
}
