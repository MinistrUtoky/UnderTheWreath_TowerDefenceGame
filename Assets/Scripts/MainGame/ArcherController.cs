using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;

public class ArcherController : MonoBehaviour
{
    [SerializeField] private AnimationCurve curvePowerByDistance;
    
    private BowController _bowController;

    public bool IsReadyToShoot => _bowController.IsCharging;
    
    private SpriteRenderer _bodySprite;
    
    private void Awake()
    {
        
        _bowController = GetComponentInChildren<BowController>();
        _bodySprite = transform.Find("BodySprite").GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        TurnAround();
    }

    public List<Arrow> Shoot() => _bowController.Shoot();

    public void Aim(Vector2 shootingVector)
    {
        _bowController.Rotate(shootingVector);
        _bowController.Charge(curvePowerByDistance.Evaluate(shootingVector.magnitude));
    }

    public void StopAiming()
    {
        _bowController.ReturnToDefault();
        _bowController.CancelCharging();
    }

    private void TurnAround()
    {
        _bodySprite.flipX = _bowController.ShootingDirection.x <= 0;
    }
}
