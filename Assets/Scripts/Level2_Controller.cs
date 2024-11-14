using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

public class Level2_Controller : MonoBehaviour
{
    //Gracz
    private MovementController playerController;
    private Rigidbody rb;

    //Canvas
    public Text timeToStartText;
    private float timeStart = 3f;
    public Text lifeText;

    public GameObject door;

    //Checkpoint i jego vector
    private Vector3 lastCheckpoint = new Vector3 (0, 0.5f, -10);
    private bool checkpoint1 = false;

    void Start()
    {
        //Wyzerowanie tekstu odliczania do nastêpnej rundy
        timeToStartText.text = "";

        //Pobranie obiektu gracza
        GameObject player = GameObject.FindWithTag("Player");
        if (player != null)
        {
            //Pobranie kontrollera gracza
            playerController = player.GetComponent<MovementController>();
            //Pobranie rigidbody obiektu
            rb = playerController.GetComponent<Rigidbody>();
        }
        else
        {
            Debug.Log("Obiekt Player nie zosta³ znaleziony!");
        }
    }

    void Update()
    {
        //Sprawdzanie przegranej
        if(playerController.GetLife() == 0)
        {
            playerController.gameOver();
        }

        //Odblokowanie przejœcia
        if (playerController.GetScore() == 4)
        {
            door.SetActive(false);
        }

        //Sprawdzanie wygranej
        if (playerController.GetScore() >= 11)
        {
            playerController.winTextGameObject.SetActive(true);
            rb.isKinematic = true;

            //Odliczanie do nastêpnej rundy
            if(timeStart > 0)
            {
                timeStart -= Time.deltaTime;
                timeToStartText.text = Mathf.CeilToInt(timeStart).ToString();
            }

            if (timeStart < 1)
            {
                //Za³adowanie nowej sceny
                //SceneManager.LoadScene("Level3", LoadSceneMode.Single);
            }
        }

        //Checkpoint i powrót po wypadniêciu z mapy
        if( checkpoint1 == false)
        {
            if (playerController.transform.position.y < -2)
            {
                playerController.transform.position = lastCheckpoint;
                playerController.DecLife();
                lifeText.text = "Life: " + playerController.GetLife().ToString();
                
                //zatrzymanie prêdkoœci playera
                rb.velocity = Vector3.zero;
                //zatrzymanie prêdkoœci k¹towej playera
                rb.angularVelocity = Vector3.zero;
            }
        }
        else
        {
            if (playerController.transform.position.y < -2)
            {
                playerController.transform.position = lastCheckpoint;
                playerController.DecLife();
                lifeText.text = "Life: " + playerController.GetLife().ToString();

                //zatrzymanie prêdkoœci playera
                rb.velocity = Vector3.zero;
                //zatrzymanie prêdkoœci k¹towej playera
                rb.angularVelocity = Vector3.zero;
            }
        }

    }

    public void SetVector3(float x, float y, float z)
    {
        lastCheckpoint = new Vector3(x, y, z);
    }
}
