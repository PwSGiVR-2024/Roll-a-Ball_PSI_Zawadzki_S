using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewMovementController : MonoBehaviour
{
    // Rigidbody
    private Rigidbody rb;

    // Si³a poruszania siê
    [SerializeField]
    private float thrust = 10.0f;

    [SerializeField]
    private float rotationSpeed = 100.0f;

    // Skakanie
    private bool isJumping;

    // Kamera
    private Transform cameraTransform;

    void Start()
    {
        rb = GetComponent<Rigidbody>();

        // Pobranie transformu kamery
        cameraTransform = Camera.main.transform; // Zak³ada, ¿e kamera ma ustawiony tag "MainCamera"

        isJumping = true;

        FindAnyObjectByType<KillBoxScript>().OnKill += ResetPostionToLastCheckpoint;
        FindAnyObjectByType<KillBoxScript>().OnKill += ResetVelocities;
    }

    void FixedUpdate()
    {
        GetInputAndUpdatePosition();
    }

    private void GetInputAndUpdatePosition()
    {
        //Pobranie kierunku w oparciu o kamerê
        Vector3 forward = cameraTransform.forward;
        Vector3 right = cameraTransform.right;

        //Y = 0, aby player porousza³ siê tylko po XZ
        forward.y = 0;
        right.y = 0;

        forward.Normalize();
        right.Normalize();

        //Poruszanie siê gracza
        Vector3 direction = Vector3.zero;

        if (Input.GetKey(KeyCode.W))
        {
            direction += forward;
        }
        if (Input.GetKey(KeyCode.S))
        {
            direction -= forward;
        }
        if (Input.GetKey(KeyCode.A)) 
        { 
            direction -= right;
        }
        if (Input.GetKey(KeyCode.D)) 
        { 
            direction += right; 
        }

        rb.AddForce(direction * thrust, ForceMode.Force);

        //obracanie gracza w kierunku ruchu
        if (direction != Vector3.zero)
        {
            transform.rotation = Quaternion.LookRotation(direction, Vector3.up);
        }


        //skakanie
        if (Input.GetKey(KeyCode.Space) && isJumping)
        {
            rb.AddForce(0, 20 * thrust, 0, ForceMode.Force);
            isJumping = false;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        //reset mozliwosci skakanie
        if (collision.gameObject.CompareTag("Ground"))
        {
            isJumping = true;
        }
    }
    public void ResetVelocities()
    {
        //zatrzymanie predkosci playera
        rb.linearVelocity = Vector3.zero;
        //zatrzymanie predkosci katowej playera
        rb.angularVelocity = Vector3.zero;
    }

    public void DisbaleRigidbody()
    {
        rb.isKinematic = true;
    }

    public void ResetPostionToLastCheckpoint()
    {
        transform.position = CheckpointScript.lastCheckpoint;
    }
}
