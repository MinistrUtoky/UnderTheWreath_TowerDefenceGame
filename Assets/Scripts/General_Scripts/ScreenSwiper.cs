using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenSwiper : MonoBehaviour
{
    
    private Vector3 origin;
    private Vector3 diff;
    private Vector3 cameraReset;
    private bool drag = false;

    void Start()
    {
        cameraReset = Camera.main.transform.position;
    }

    private void LateUpdate()
    {
        if (Input.GetMouseButton(0))
        {
            diff = (Camera.main.ScreenToWorldPoint(Input.mousePosition)) - Camera.main.transform.position;
            if (!drag)
            {
                drag = true;
                origin = (Camera.main.ScreenToWorldPoint(Input.mousePosition));
            }
        }
        else
        {
            drag = false;
        }
        if (drag)
        {
            Camera.main.transform.position = origin - diff;
        }
        if (Input.GetMouseButton(1))
        {
            Camera.main.transform.position = cameraReset;
        }
    }   
}
