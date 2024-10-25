using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MovementController : MonoBehaviour
{

    private Rigidbody rb;
    private int score = 0;

    [SerializeField]
    private float thrust = 10.0f;

    public Text scoreText;
    public GameObject winTextGameObject;

    private bool isJumping;

    void Start()
    {
        scoreText.text = "Score: " + 0;
        winTextGameObject.SetActive(false);
        isJumping = true;
    }

    void Update()
    {
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
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

        if(Input.GetKey(KeyCode.Space) && isJumping) {
            rb.AddForce( 0, 20 * thrust , 0, ForceMode.Force);
            isJumping = false;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isJumping = true;
        }
    }

    public void SetScore()
    {
        score++;
        scoreText.text = "Score: " + score;

        if (score >= 10) {
            winTextGameObject.SetActive(true);
            SceneManager.LoadScene("Level2", LoadSceneMode.Single);
        }
    }

    public int GetScore()
    {
       return score;
    }
}
