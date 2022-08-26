using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Melee : BasicEnemy
{
    void OnTriggerEnter2D(Collider2D col)
    {
        _obj = col.gameObject;
        if (_obj.tag == "Building")
        {
            rb.velocity = Vector2.zero;
            StartCoroutine(HittingBuiling(_obj));
        }
        if (_obj.tag == "Builder") _obj.GetComponent<Builder>().Retreat();
    }

    void OnTriggerExit2D(Collider2D col)
    {
        _obj = col.gameObject;
        if (_obj.tag == "Building")
        {
            StopCoroutine(HittingBuiling(_obj));
            rb.velocity = new Vector3(speed, 0, 0);
        }
    }
}
