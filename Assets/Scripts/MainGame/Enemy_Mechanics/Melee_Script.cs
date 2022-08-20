using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Melee_Script : Basic_Enemy_Script
{
    void OnCollisionEnter2D(Collision2D col)
    {
        GameObject obj = col.gameObject;
        if (obj.name == "Barracks" || obj.name == "Townhall" || obj.name == "Wall")
        {
            rb.velocity = Vector2.zero;
            StartCoroutine(HittingBuiling(obj));
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
            rb.velocity = new Vector3(speed, 0, 0);
        }
    }
}
