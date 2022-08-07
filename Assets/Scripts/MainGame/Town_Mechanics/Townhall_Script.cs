using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Townhall_Script : Building_Script
{
    public void TakeDamage(int damage)
    {
        _hp -= damage;
        if (_hp <= 0)
            SceneManager.LoadScene(2, LoadSceneMode.Single);
    }
}
