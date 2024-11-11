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
        GameObject player = GameObject.FindWithTag("Player");

        if (player != null)
        {
            playerController = player.GetComponent<MovementController>();
        }
        else
        {
            Debug.Log("Obiekt Player nie zosta³ znaleziony!");
        }
    }

    void Update()
    {
        if (playerController.GetScore() >= 10)
        {
            playerController.winTextGameObject.SetActive(true);
            timeStart -= Time.deltaTime;
            timeToStartText.text = Mathf.CeilToInt(timeStart).ToString();

            if(timeStart < 1)
            {
                SceneManager.LoadScene("Level2", LoadSceneMode.Single);
            }
        }
    }
}
