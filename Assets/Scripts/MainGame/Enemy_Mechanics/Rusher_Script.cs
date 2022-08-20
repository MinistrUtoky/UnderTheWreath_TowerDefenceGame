using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rusher_Script : Basic_Enemy_Script
{
    [SerializeField] private float _damageBackLeap = 1000f;
    private Vector3 _backLeapVector;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        _backLeapVector = new Vector3(-_damageBackLeap, _damageBackLeap, 0);
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        GameObject obj = col.gameObject;
        if (obj.name == "Barracks" || obj.name == "Townhall" || obj.name == "Wall")
        {
            rb.velocity = Vector2.zero;
            if (obj.name == "Wall")
            {
                rb.AddForce(_backLeapVector);
                DamageDealing(obj);
            }
            else StartCoroutine(HittingBuiling(obj));
        }
        if (obj.name == "Ground")
        {
            rb.velocity = new Vector3(speed, 0, 0);
        }
    }

    void OnCollisionExit2D(Collision2D col)
    {
        GameObject obj = col.gameObject;
        if (obj.name == "Barracks" || obj.name == "Townhall" || obj.name == "Wall")
        {
            StopCoroutine(HittingBuiling(obj));
            if (obj.name != "Wall") rb.velocity = new Vector3(speed, 0, 0);
        }
    }
}
