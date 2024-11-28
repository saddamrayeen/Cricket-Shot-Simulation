using UnityEngine;
using Cinemachine;
using System;
using System.Collections;
using System.Collections.Generic;
public class CameraController : MonoBehaviour
{
    public CinemachineVirtualCamera followCam;
    public CinemachineVirtualCamera rotateCam;
    public Rigidbody ballRigidbody;
    public LineRenderer trajectoryLine;
    public float lineRevealDelay = 1f;
    public float rotateCamDelay = 2f;
    void Start()
    {
        trajectoryLine.enabled = false;
    }
    public void OnStumpHit()
    {
        StartCoroutine(HandleCameraSwitch());
    }

    IEnumerator HandleCameraSwitch()
    {
        // Wait and reveal the trajectory line
        yield return new WaitForSeconds(lineRevealDelay);
        trajectoryLine.enabled = true;

        // Wait and switch to the rotating camera
        yield return new WaitForSeconds(rotateCamDelay);
        followCam.Priority = 0;
        rotateCam.Priority = 10;
    }
}
