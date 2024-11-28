using System.Collections;
using Cinemachine;
using UnityEngine;

public class CameraRotate : MonoBehaviour
{
    public Transform target;       
    public float rotationSpeed = 10f;

    private void Start()
    {
        if (gameObject.name == "OverlookCamera")
        {
            StartCoroutine(TriggerCameraChange());
        }
    }
    void Update()
    {
        transform.RotateAround(target.position, Vector3.up, rotationSpeed * Time.deltaTime);
    }
    IEnumerator TriggerCameraChange()
    {
        yield return new WaitForSeconds(10);
        GetComponent<CinemachineVirtualCamera>().Priority = 0;
        GameObject.FindWithTag("FollowCamera").GetComponent<CinemachineVirtualCamera>().Priority = 10;
    }

}
