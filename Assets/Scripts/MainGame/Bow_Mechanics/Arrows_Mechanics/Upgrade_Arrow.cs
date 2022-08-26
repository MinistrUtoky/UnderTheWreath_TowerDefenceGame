using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Upgrade_Arrow : Arrow
{
    private GameObject _buildingToUpgrade;
    private float _leftSide;
    private float _rightSide;

    void OnTriggerEnter2D(Collider2D col)
    {
        _obj = col.gameObject;
        if (_obj.name == "Ground")
        {
            Land();
            if (_buildingToUpgrade != null)
            {
                if (transform.position.x > _leftSide && transform.position.x < _rightSide)
                    _buildingToUpgrade.GetComponent<Building_Script>().LevelUp();
            }
        }
        if (_obj.tag == "Enemy")
        {
            if (_col.enabled == false) return;
            _col.enabled = false;
            _obj.GetComponent<Basic_Enemy_Script>().TakeDamage(_damage);
            Destroy(gameObject);
        }
        if (_obj.tag == "Building")
        {
            _buildingToUpgrade = _obj;
            _leftSide = _obj.transform.position.x - _obj.transform.localScale.x / 2;
            _rightSide = _obj.transform.position.x + _obj.transform.localScale.x / 2;
        }
    }
}
