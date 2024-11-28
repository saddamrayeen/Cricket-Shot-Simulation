using System;
using System.Collections.Generic;
using UnityEngine;
using System.Collections;


public class TrejectoryDrawer : MonoBehaviour
{
    public LineRenderer lineRenderer;
    public Rigidbody ballRigidbody;
    public float updateInterval = 0.1f;

    private float timer = 0f;
    private bool isTracking = false;

    public List<Vector3> points;

    void Start()
    {
        lineRenderer.enabled = false;
        lineRenderer.positionCount = 0; // Initialize with no points
    }

    void Update()
    {

        if (Input.GetKeyDown(KeyCode.F))
        {
            StartTracking();
            StartCoroutine(RevealTrajectory());
        }


        if (isTracking)
        {
            timer += Time.deltaTime;
            if (timer >= updateInterval)
            {
                UpdateTrajectory();
                timer = 0f;
            }
        }
    }

    void StartTracking()
    {
        lineRenderer.positionCount = 0;
        isTracking = true;
        AddPoint();
    }



    void UpdateTrajectory()
    {
        AddPoint();

        if (ballRigidbody.velocity.magnitude < 0.05f)
        {
            isTracking = false;

        }

    }

    void AddPoint()
    {
        points.Add(ballRigidbody.position);
    }

    private IEnumerator RevealTrajectory()
    {
        yield return new WaitForSeconds(3.5f);
        // lineRenderer.SetPosition(pointCount, currentPosition);
        lineRenderer.enabled = true;
        for (int i = 0; i < points.Count; i++)
        {
            lineRenderer.positionCount = i + 1;
            lineRenderer.SetPosition(i, points[i]);

            // Wait for the specified delay before revealing the next point
            yield return new WaitForSeconds(.2f);
        }


    }
}
