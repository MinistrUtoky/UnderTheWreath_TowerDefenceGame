using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeArrow : Arrow
{
    private GameObject _buildingToUpgrade;

    void OnTriggerEnter2D(Collider2D col)
    {
        _obj = col.gameObject;
        if (_obj.name == "Ground")
        {
            Land();
            if (_buildingToUpgrade != null)
            {
                _buildingToUpgrade.GetComponent<Building>().LevelUp();
            }
        }
        if (_obj.tag == "Enemy")
        {
            if (_col.enabled == false) return;
            _col.enabled = false;
            _obj.GetComponent<BasicEnemy>().TakeDamage(damage);
            Destroy(gameObject);
        }
        if (_obj.tag == "Building")
        {
            _buildingToUpgrade = _obj;
        }
    }
}
