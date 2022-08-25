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

    void OnTriggerEnter2D(Collider2D col)
    {
        _obj = col.gameObject;
        if (_obj.tag == "Building")
        {
            rb.velocity = Vector2.zero;
            if (_obj.name == "Wall")
            {
                rb.AddForce(_backLeapVector);
                DamageDealing(_obj);
            }
            else StartCoroutine(HittingBuiling(_obj));
        }
        if (_obj.tag == "Builder") _obj.GetComponent<Builder_Script>().Retreat();
    }


    void OnTriggerExit2D(Collider2D col)
    {
        _obj = col.gameObject;
        if (_obj.tag == "Building")
        {
            StopCoroutine(HittingBuiling(_obj));
            if (_obj.name != "Wall") rb.velocity = new Vector3(speed, 0, 0);
        }
    }
}
