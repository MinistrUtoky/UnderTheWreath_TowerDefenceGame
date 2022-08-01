using UnityEngine;

public class ScreenSwiper : MonoBehaviour
{
    
    private Vector3 _origin;
    private Vector3 _diff;
    private Vector3 _cameraReset;
    private bool _drag = false;
    private Camera _mainCamera;

    private void Awake()
    {
        _mainCamera = Camera.main;
    }

    void Start()
    {
        _cameraReset = _mainCamera.transform.position;
    }

    private void LateUpdate()
    {
        if (Input.GetMouseButton(0))
        {
            _diff = (_mainCamera.ScreenToWorldPoint(Input.mousePosition)) - _mainCamera.transform.position;
            if (!_drag)
            {
                _drag = true;
                _origin = (_mainCamera.ScreenToWorldPoint(Input.mousePosition));
            }
        }
        else
        {
            _drag = false;
        }
        if (_drag)
        {
            _mainCamera.transform.position = _origin - _diff;
        }
        if (Input.GetMouseButton(1))
        {
            _mainCamera.transform.position = _cameraReset;
        }
    }   
}
