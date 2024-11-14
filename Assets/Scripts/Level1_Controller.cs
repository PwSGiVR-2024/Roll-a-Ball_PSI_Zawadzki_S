using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Level1_Controller : MonoBehaviour
{
    private MovementController playerController;

    public Text timeToStartText;
    private float timeStart = 3f;

    void Start()
    {
        timeToStartText.text = "";

        //Pobranie obiektu gracza
        GameObject player = GameObject.FindWithTag("Player");
        if (player != null)
        {
            //Pobranie kontrollera gracza
            playerController = player.GetComponent<MovementController>();
        }
        else
        {
            Debug.Log("Obiekt Player nie zosta³ znaleziony!");
        }
    }

    void Update()
    {
        //Sprawdzanie wygranej
        if (playerController.GetScore() >= 10)
        {
            playerController.winTextGameObject.SetActive(true);

            //Odliczanie do nastêpnej rundy
            if(timeStart > 0)
            {
                timeStart -= Time.deltaTime;
                timeToStartText.text = Mathf.CeilToInt(timeStart).ToString();
            }
            
            //Za³adowanie nowej sceny
            if(timeStart < 1)
            {
                SceneManager.LoadScene("Level2", LoadSceneMode.Single);
            }
        }
    }
}
