using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallMovement : MonoBehaviour
{
    public Transform bowlerEnd;
    public Transform batterEnd;
    public float speed = 5f;
    private bool isMoving = false;

    AudioSource audioSource;
    public AudioClip ballJump, stumpHit;

    public List<GameObject> allStamps;
    Rigidbody ballRigidBody;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        ballRigidBody = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            StartMovement();
        }
    }

    void FixedUpdate()
    {
        if (isMoving)
        {
            ballRigidBody.isKinematic = false;
            ballRigidBody.useGravity = true;
            transform.position = Vector3.MoveTowards(transform.position, batterEnd.position, speed * Time.deltaTime);
            if (Vector3.Distance(transform.position, batterEnd.position) < 0.1f)
            {
                isMoving = false;
            }
        }
    }

    public void StartMovement()
    {
        transform.position = bowlerEnd.position;
        isMoving = true;
    }
    private void OnCollisionEnter(Collision other)
    {

        if (other.gameObject.tag == "Ground")
        {
            Time.timeScale = Mathf.Lerp(.2f, .1f, .2f);
            audioSource.clip = ballJump;
            audioSource.Play();
        }
        if (other.gameObject.tag == "Stamp")
        {
            isMoving = false;
            foreach (var stamp in allStamps)
            {
                var StampRB = stamp.GetComponent<Rigidbody>();
                StartCoroutine(MoveStump(StampRB));
            }
            ballRigidBody.isKinematic = true;
            ballRigidBody.useGravity = false;

            audioSource.clip = stumpHit;
            audioSource.Play();
            FindObjectOfType<CameraController>().OnStumpHit();
        }
    }
    IEnumerator MoveStump(Rigidbody _rb)
    {
        _rb.AddForce(new Vector3(-Random.Range(.3f, 1), -Random.Range(.3f, 1), -Random.Range(.3f, 1)), ForceMode.Impulse);
        _rb.constraints = RigidbodyConstraints.None;
        _rb.isKinematic = false;
        _rb.useGravity = true;

        yield return new WaitForSeconds(.2f);

        _rb.constraints = RigidbodyConstraints.FreezeAll;
        _rb.isKinematic = true;
        _rb.useGravity = false;
    }
}
