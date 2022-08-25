using System;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class TrajectoryController : MonoBehaviour
{
    [SerializeField] private float maxDistance;
    [SerializeField] private float timeStep;
    [SerializeField] private Transform throwingPoint;

    private LineRenderer _trajectoryLine;
    
    private void Awake()
    {
        _trajectoryLine = GetComponent<LineRenderer>();
    }

    public void DrawLine(float speed, Vector2 direction)
    {
        var dots = new List<Vector3>();

        direction = direction.normalized;
        float cosAlpha = direction.x;
        float sinAlpha = direction.y;
        float velocityOx = speed * cosAlpha;
        float velocityOy = speed * sinAlpha;
        float x0 = throwingPoint.position.x;
        float y0 = throwingPoint.position.y;
        float g = Physics2D.gravity.y;

        dots.Add(new Vector3(x0, y0, 0));
        float sumDistance = 0;
        float timeElapsed = 0;
        Vector3 previousDot = dots[0];
        while (sumDistance < maxDistance)
        {
            float t = timeElapsed + timeStep;
            float x = x0 + velocityOx * t;
            float y = y0 + velocityOy * t + g * t * t / 2;
            timeElapsed = t;
            Vector3 curDot = new Vector3(x, y, 0);
            sumDistance += Vector3.Distance(curDot, previousDot);
            dots.Add(curDot);
            previousDot = curDot;
        }
        
        // Vector3 previousDot = new Vector3(x0, y0, 0);
        // Vector3 curDot;
        // RaycastHit2D raycast;
        // do
        // {
        //     float curX = previousDot.x + parabolaDirection * distanceBetweenChecks;
        //     float curY = g * math.pow(curX - x0, 2) / (2 * velocityOX * velocityOX) +
        //                  velocityOY * (curX - x0) / velocityOX + y0;
        //     curDot = new Vector3(curX, curY, 0);
        //     raycast = Physics2D.Raycast(previousDot, curDot - previousDot, Vector3.Distance(previousDot, curDot));
        //     previousDot = curDot;
        // } while (raycast.collider == null && raycast.transform.gameObject.CompareTag("Ground"));
        //
        // float endPoint = raycast.point.x;
        // float distanceBetweenDots = math.abs(endPoint - x0) / dotCount;
        // for (int i = 0; i < dotCount; i++)
        // {
        //     float curX = x0 + parabolaDirection * distanceBetweenDots * i;
        //     float curY = g * math.pow(curX - x0, 2) / (2 * velocityOX * velocityOX) +
        //                  velocityOY * (curX - x0) / velocityOX + y0;
        //     dots[i] = new Vector3(curX, curY, 0);
        // }
        _trajectoryLine.positionCount = dots.Count;
        _trajectoryLine.SetPositions(dots.ToArray());
    }

    public void ClearLine()
    {
        _trajectoryLine.positionCount = 0;
    }
}
