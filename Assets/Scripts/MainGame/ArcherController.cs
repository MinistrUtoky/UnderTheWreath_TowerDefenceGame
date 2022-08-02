using System;
using System.Collections.Generic;
using UnityEngine;

public class ArcherController : MonoBehaviour
{
    private BowController _bowController;
    public bool IsReadyToShoot => _bowController.IsCharging;
    
    private SpriteRenderer _bodySprite;

    public void SetDefaultPosition(Vector2 rotationDirection) => _bowController.SetDefaultRotation(rotationDirection);
    
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
        _bowController.Charge();
    }

    public void StopAiming()
    {
        _bowController.RotateToDefaultPosition();
        _bowController.StopCharging();
    }

    private void TurnAround()
    {
        _bodySprite.flipX = _bowController.ShootingVector.x <= 0;
    }
}
