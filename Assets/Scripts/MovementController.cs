using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngineInternal;

public class MovementController : MonoBehaviour
{
    //Rigidbody
    private Rigidbody rb;

    //Si³a poruszania siê
    [SerializeField]
    private float thrust = 10.0f;

    //Skakanie
    private bool isJumping;

    void Start()
    {
        isJumping = true;

        FindAnyObjectByType<KillBoxScript>().OnKill += ResetPostionToLastCheckpoint;
        FindAnyObjectByType<KillBoxScript>().OnKill += ResetVelocities;
    }

    void Update()
    {
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        GetInputAndUpdatePosition();
    }

    private void GetInputAndUpdatePosition()
    {
        if (Input.GetKey(KeyCode.W))
        {
            rb.AddForce(0, 0, thrust, ForceMode.Force);
        }

        if (Input.GetKey(KeyCode.S))
        {
            rb.AddForce(0, 0, -1 * thrust, ForceMode.Force);
        }

        if (Input.GetKey(KeyCode.A))
        {
            rb.AddForce(-1 * thrust, 0, 0, ForceMode.Force);
        }

        if (Input.GetKey(KeyCode.D))
        {
            rb.AddForce(thrust, 0, 0, ForceMode.Force);
        }

        //Brak mo¿liwoœci podskakiwania w powietrzu
        if (Input.GetKey(KeyCode.Space) && isJumping)
        {
            rb.AddForce(0, 20 * thrust, 0, ForceMode.Force);
            isJumping = false;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        //Reset mo¿liwoœci skakanie
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
