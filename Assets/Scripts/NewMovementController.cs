using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewMovementController : MonoBehaviour
{
    //rigidbody
    private Rigidbody rb;

    //si³a poruszania siê
    [SerializeField]
    private float thrust = 10.0f;

    //skakanie
    private bool isJumping;

    //kamera
    private Transform cameraTransform;

    //obikety
    private KillBoxScript killBox;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        isJumping = true;

        //pobranie transformu kamery
        cameraTransform = Camera.main.transform; //zak³ada, ¿e kamera ma ustawiony tag "MainCamera"

        //Przypisanie eventów
        killBox = FindFirstObjectByType<KillBoxScript>();
        if (killBox != null)
        {
            killBox.OnKill += ResetPostionToLastCheckpoint;
            killBox.OnKill += ResetVelocities;
        }
    }
    
    void FixedUpdate()
    {
        GetInputAndUpdatePosition();
    }

    private void GetInputAndUpdatePosition()
    {
        //pobranie kierunku w oparciu o kamerê
        Vector3 forward = cameraTransform.forward;
        Vector3 right = cameraTransform.right;

        //Y = 0, aby player porousza³ siê tylko po XZ
        forward.y = 0;
        right.y = 0;

        forward.Normalize();
        right.Normalize();

        //poruszanie siê gracza
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
    public void ApplyKnockback(Vector3 direction, float force)
    {
        
        rb.AddForce(new Vector3(direction.x, 0.5f, direction.z) * force, ForceMode.Impulse);
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

    public void DisableRigidbody()
    {
        rb.isKinematic = true;
    }

    public void ResetPostionToLastCheckpoint()
    {
        transform.position = CheckpointScript.lastCheckpoint;
    }

    private void OnDisable()
    {
        if (killBox != null)
        {
            killBox.OnKill -= ResetPostionToLastCheckpoint;
            killBox.OnKill -= ResetVelocities;
        }
    }
}
