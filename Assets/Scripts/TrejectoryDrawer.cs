using UnityEngine;

public class TrejectoryDrawer : MonoBehaviour
{
    public LineRenderer lineRenderer;
    public Rigidbody ballRigidbody;
    public float updateInterval = 0.1f;

    private float timer = 0f;
    private bool isTracking = false;

    void Start()
    {
        lineRenderer.enabled = false;
        lineRenderer.positionCount = 0; // Initialize with no points
    }

    void Update()
    {
       
        if (Input.GetKeyDown(KeyCode.F))
            StartTracking();

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
            isTracking = false;
    }

    void AddPoint()
    {
        Vector3 currentPosition = ballRigidbody.position;

        int pointCount = lineRenderer.positionCount;
        lineRenderer.positionCount = pointCount + 1;
        lineRenderer.SetPosition(pointCount, currentPosition);
    }


}
