using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public event Action pickUpEvent;
    public event Action bgMusicSoundEvent;
    public event Action bgSoundEventStop;

    public event Action gameOverSoundEvent;
    private bool gameOverSound = true;
    
    //Gracz
    private MovementController playerController;
    private Rigidbody rb;

    //punkty w grze
    private int playerScore = 0;
    private int playerLife = 1;

    //maksymalna liczba puntk�w do zdobycia
    private GameObject[] collectibles;
    private int maxScore;

    //numer sceny
    private int numberOfScene;

    //public Text timeToStartText;
    private float timeStart = 3f;

    private UIController uiController;


    void Start()
    {
        //Pobranie kontrollera UI
        uiController = gameObject.GetComponent<UIController>();
        uiController.winTextGameObject.SetActive(false);
        uiController.gameOverGameObject.SetActive(false);
        uiController.timeToStartText.text = "";

        //Wyzerowanie punkt�w gracza w ka�dym poziomie
        playerScore = 0;

        //Okre�lenie maksymalnej ilo�ci punkt�w do zdobycia w ka�dym poziomie
        collectibles = GameObject.FindGameObjectsWithTag("Collectible");
        maxScore = collectibles.Length;

        //Okre�lenie obecnego numeru sceny
        numberOfScene = SceneManager.GetActiveScene().buildIndex;
        
        //Wy�wietelenie ilo�ci �y� gracza
        uiController.lifeText.text = "Life: " + playerLife;

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
            Debug.Log("Obiekt Player nie zosta� znaleziony!");
        }
    }

    void Update()
    {
        //Sprawdzanie przegranej
        if (playerLife == 0)
        {
            GameOver();
        }

        //Powr�t na plansze po wypadni�ciu
        if (playerController.transform.position.y < -2)
        {
            playerController.transform.position = CheckpointScript.lastCheckpoint;
            DecLife();
            uiController.lifeText.text = "Life: " + playerLife;

            //zatrzymanie predkosci playera
            rb.linearVelocity = Vector3.zero;
            //zatrzymanie predkosci katowej playera
            rb.angularVelocity = Vector3.zero;
        }

        checkWin();
    }

    private void checkWin()
    {
        if (playerScore >= maxScore)
        {
            uiController.winTextGameObject.SetActive(true);
            rb.isKinematic = true;

            //Odliczanie do nast�pnej rundy
            if (timeStart > 0)
            {
                timeStart -= Time.deltaTime;
                uiController.timeToStartText.text = Mathf.CeilToInt(timeStart).ToString();
            }

            //Za�adowanie nowej sceny
            if (timeStart < 1)
            {
                SceneManager.LoadScene(numberOfScene + 1, LoadSceneMode.Single);
            }
        }
    }

    //Ustawianie wyniku
    public void SetScore()
    {
        playerScore++;
        pickUpEvent?.Invoke();

        //Zamiast tego zrobiony pickUpEvent
        //uiController.scoreText.text = "Score: " + playerScore.ToString();
    }

    public int GetScore()
    {
        return playerScore;
    }

    //Dekrementacja �ycia
    public void DecLife()
    {
        playerLife--;
        uiController.lifeText.text = "Life: " + playerLife;
    }

    private void GameOver()
    {
        if (gameOverSound)
        {
            gameOverSoundEvent?.Invoke();
            gameOverSound = false;
        }
        rb.isKinematic = true;
        uiController.gameOverGameObject.SetActive(true);

        Invoke(nameof(LoadNextScene), 2.0f);
    }

    private void LoadNextScene()
    {
        //Odliczanie do nast�pnej rundy
        if (timeStart > 0)
        {
            timeStart -= Time.deltaTime;
            uiController.timeToStartText.text = Mathf.CeilToInt(timeStart).ToString();
        }

        //Za�adowanie ko�cowej sceny
        if (timeStart < 1)
        {
            SceneManager.LoadScene("End Scene", LoadSceneMode.Single);
        }
    }
}
