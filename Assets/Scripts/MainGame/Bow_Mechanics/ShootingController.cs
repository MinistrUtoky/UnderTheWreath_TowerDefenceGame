using System;
using UnityEngine;

public class ShootingController : MonoBehaviour
{
    [SerializeField] private GameObject dotStart;
    [SerializeField] private GameObject dotEnd;
    
    private ArcherController _archer;
    
    private Vector2 _dragStartPoint;
    private Vector2 _dragEndPoint;
    private Camera _mainCamera;

    private void Awake()
    {
        _mainCamera = Camera.main;
    }

    private void Start()
    {
        _archer = LevelManager.Instance.Archer;
    }

    private void Update()
    {
        // if (Input.touchCount == 1)
        // {
        //     Touch touch = Input.GetTouch(0);
        //     Vector2 touchPosition = _mainCamera.ScreenToWorldPoint(touch.position);
        //     if (touch.phase == TouchPhase.Began)
        //     {
        //         OnBeginDrag(touchPosition);
        //     } 
        //     else if (touch.phase == TouchPhase.Moved)
        //     {
        //         OnDrag(touchPosition);
        //     } 
        //     else if (touch.phase == TouchPhase.Ended)
        //     {
        //         OnEndDrag(touchPosition);
        //     }
        // }
        if (Input.GetMouseButtonDown(0))
        {
            OnBeginDrag(Input.mousePosition);
        } 
        else if (Input.GetMouseButton(0))
        {
            OnDrag(Input.mousePosition);
        }
        else if (Input.GetMouseButtonUp(0))
        {
            OnEndDrag(Input.mousePosition);
        }
    }

    private void OnBeginDrag(Vector3 touchPosition)
    {
        _dragStartPoint = _mainCamera.ScreenToWorldPoint(touchPosition);
        dotStart.transform.position = _dragStartPoint;
        _dragEndPoint = _dragStartPoint;
    }

    private void OnDrag(Vector3 touchPosition)
    {

        _dragEndPoint = _mainCamera.ScreenToWorldPoint(touchPosition);
        dotEnd.transform.position = _dragEndPoint;
        // Debug.Log(Vector2.Distance(dotEnd.transform.position, dotStart.transform.position));
        if (_dragEndPoint == _dragStartPoint)   
        {
            _archer.StopAiming();
        }
        else
        {
            _archer.Aim(_dragStartPoint - _dragEndPoint);
        }
    }

    private void OnEndDrag(Vector3 touchPosition)
    {
        if (_dragEndPoint == _dragStartPoint)
        {
            if (_archer.IsReadyToShoot)
            {
                _archer.StopAiming();
            }
        }
        else 
        {
            _dragEndPoint = _mainCamera.ScreenToWorldPoint(touchPosition);
            _archer.Shoot();
            _dragEndPoint = Vector2.zero;
            _dragStartPoint = Vector2.zero;
        }
    }
}
