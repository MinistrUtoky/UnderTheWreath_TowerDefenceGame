using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using Quaternion = UnityEngine.Quaternion;
using Vector2 = UnityEngine.Vector2;
using Vector3 = UnityEngine.Vector3;

public class BowController : MonoBehaviour
{
    [SerializeField] private GameObject arrowPrefab;
    
    private SpriteRenderer _bowSprite;

    private Transform _arrowSpawningPoint;
    [SerializeField] private AnimationCurve curvePowerByDistance;

    public Vector2 ShootingVector => _shootingVector;

    private Vector2 _shootingVector;
    
    private Vector2 _defaultShootingVector;

    private bool _isCharging = false;

    public bool IsCharging => _isCharging;
    private Animator anim;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        _bowSprite = transform.Find("BowSprite").GetComponent<SpriteRenderer>();
        _arrowSpawningPoint = transform.Find("ArrowSpawningPoint");
    }

    public void SetDefaultRotation(Vector2 rotationDirection)
    {
        if (rotationDirection == Vector2.zero)
        {
            Debug.LogError("Attempt to set zero default direction");
            return;
        }
        _defaultShootingVector = rotationDirection;
        _isCharging = false;
        _shootingVector = _defaultShootingVector;
    }

    public void Rotate(Vector2 rotationDirection)
    {
        if (rotationDirection == Vector2.zero)
        {
            Debug.LogError("Attempt to rotate to zero direction");
            return;
        }
        _shootingVector = rotationDirection;
        transform.rotation = Quaternion.LookRotation(Vector3.forward, _shootingVector);
        // transform.rotation.SetLookRotation(transform.position + (Vector3)_rotationDirection);
    }

    public void Charge()
    {
        if (IsCharging)
            return;
        _isCharging = true;
        
        //TODO charging animation
    }

    public List<Arrow> Shoot()
    {
        if (!_isCharging)
        {
            Debug.LogError("Attempt to shoot without charging");
            return null;
        }
        _isCharging = false;


        var arrow = Instantiate(arrowPrefab, _arrowSpawningPoint.position,
            Quaternion.LookRotation(Vector3.forward, _shootingVector)).GetComponent<Arrow>();
        var arrows = new List<Arrow> { arrow };
        Debug.Log(_shootingVector.magnitude);
        arrow.Throw(_shootingVector.normalized * curvePowerByDistance.Evaluate(_shootingVector.magnitude));
        
        RotateToDefaultPosition();
        return arrows;
    }

    public void StopCharging()
    {
        if (!_isCharging)
            return;
        _isCharging = false;

        //TODO stop animation
    }

    public void RotateToDefaultPosition() => Rotate(_defaultShootingVector);
}
