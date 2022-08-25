using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using Quaternion = UnityEngine.Quaternion;
using Random = UnityEngine.Random;
using Vector2 = UnityEngine.Vector2;
using Vector3 = UnityEngine.Vector3;

public class BowController : MonoBehaviour
{
    [SerializeField] private GameObject arrowPrefab;
    [SerializeField] private AnimationCurve bowSpriteByChargeValueCurve;
    [SerializeField] private AnimationCurve shakingMagnitudeByChargeValueCurve;
    [SerializeField] private AnimationCurve chargeValueByPowerCurve;
    [SerializeField] private List<Sprite> bowSprites;
    
    private SpriteRenderer _bowSprite;
    private TrajectoryController _trajectory;

    private Transform _arrowSpawningPoint;

    private Vector2 _shootingDirection;
    private float _shootingPower;
    /// <summary>
    /// value between 0 and 1 which is effects on bow sprite and shaking effect
    /// </summary>
    private float _chargeValue;
    private Quaternion _originSpriteRotation;

    /// <summary>
    /// default rotation when bow isn't charging
    /// </summary>
    private Vector2 DefaultShootingVector
    {
        get
        {
            return LevelManager.Instance.DefaultRotationDirection;
        }
    }

    public bool IsCharging => _chargeValue != 0;

    public Vector2 ShootingDirection => _shootingDirection;

    private void Awake()
    {
        _trajectory = GetComponentInChildren<TrajectoryController>();
        _bowSprite = transform.Find("BowSprite").GetComponent<SpriteRenderer>();
        _arrowSpawningPoint = transform.Find("ArrowSpawningPoint");
    }

    private void Start()
    {
        ReturnToDefault();
        _originSpriteRotation = _bowSprite.transform.localRotation;
    }

    private void Update()
    {
        Shake();
        FlipBowSprite();
    }

    private void FlipBowSprite()
    {
        int spriteIndex = (int) bowSpriteByChargeValueCurve.Evaluate(_chargeValue);
        _bowSprite.sprite = bowSprites[spriteIndex];
    }

    private void Shake()
    {
        float zRotation = Random.Range(-1, 1) * shakingMagnitudeByChargeValueCurve.Evaluate(_chargeValue);
        _bowSprite.transform.localRotation = Quaternion.Euler(_originSpriteRotation.eulerAngles + new Vector3(0, 0, zRotation));
    }

    public void Rotate(Vector2 rotationDirection)
    {
        if (rotationDirection == Vector2.zero)
        {
            Debug.LogError("Attempt to rotate to zero direction");
            return;
        }
        _shootingDirection = rotationDirection;
        transform.rotation = Quaternion.LookRotation(Vector3.forward, _shootingDirection);
        Debug.Log(transform.rotation.eulerAngles);
    }

    
    public void Charge(float power)
    {
        _chargeValue = chargeValueByPowerCurve.Evaluate(power);
        _shootingPower = power;
        _trajectory.DrawLine(_shootingPower, _shootingDirection);
    }

    public List<Arrow> Shoot()
    {
        if (!IsCharging)
        {
            Debug.LogError("Attempt to shoot without charging");
            return null;
        }
        var arrow = Instantiate(arrowPrefab, _arrowSpawningPoint.position,
            Quaternion.LookRotation(Vector3.forward, _shootingDirection)).GetComponent<Arrow>();
        var arrows = new List<Arrow> { arrow };
        arrow.Throw(_shootingDirection.normalized * _shootingPower);
        ReturnToDefault();
        return arrows;
    }
    
    /// <summary>
    /// If reconsider to shoot 
    /// </summary>
    public void CancelCharging()
    {
        if (_chargeValue == 0)
        {
            return;
        }
        ReturnToDefault();
        //TODO stop animation
    }

    public void ReturnToDefault()
    {
        Rotate(DefaultShootingVector);
        _chargeValue = 0;
        _trajectory.ClearLine();
    } 
}
