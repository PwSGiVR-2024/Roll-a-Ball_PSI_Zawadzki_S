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

    //Atrybuty zawodnika
    private int score = 0;
    private int life = 3;

    //Si³a poruszania siê
    [SerializeField]
    private float thrust = 10.0f;

    //Canvas
    public Text scoreText;
    public GameObject winTextGameObject;
    public GameObject gameOverGameObject;

    //Skakanie
    private bool isJumping;

    void Start()
    {
        score = 0;
        scoreText.text = "Score: " + score;

        life = 3;

        winTextGameObject.SetActive(false);
        gameOverGameObject.SetActive(false);
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

        //Brak mo¿liwoœci podskakiwania w powietrzu
        if(Input.GetKey(KeyCode.Space) && isJumping) {
            rb.AddForce( 0, 20 * thrust , 0, ForceMode.Force);
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

    //Ustawianie wyniku
    public void SetScore()
    {
        score++;
        scoreText.text = "Score: " + score;
    }
    //Pobieranie wyniku
    public int GetScore()
    {
       return score;
    }

    //Dekrementacja ¿ycia
    public void DecLife()
    {
        life--;
    }
    //Pobieranie ¿ycia
    public int GetLife()
    {
        return life;
    }

    //Game OVer
    public void gameOver()
    {
        rb.isKinematic = true;
        gameOverGameObject.SetActive(true);
    }
}
