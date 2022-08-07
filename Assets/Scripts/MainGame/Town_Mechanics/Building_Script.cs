using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building_Script : MonoBehaviour
{
    [SerializeField] protected int _hp = 50;

    public void TakeDamage(int damage)
    {
        _hp -= damage;
        if (_hp <= 0)
            gameObject.SetActive(false);
    }
}
