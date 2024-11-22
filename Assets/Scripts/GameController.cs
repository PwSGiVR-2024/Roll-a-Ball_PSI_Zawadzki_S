using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public event Action bgMusicSoundEvent;
    public event Action bgSoundEventStop;

    public event Action gameOverSoundEvent;
    private bool gameOverSound = true;

    //Gracz fiyzka
    private MovementController movementController;
    private Rigidbody rb;

    //Gracz w³aœciwoœci
    private PlayerController playerController;

    //punkty gracza
    private int playerScore = 0;

    //maksymalna liczba puntków do zdobycia
    private GameObject[] collectibles;
    private int maxScore;

    //numer sceny
    private int numberOfScene;

    //public Text timeToStartText;
    private float timeStart = 3f;

    private UIController uiController;
    

    void Start()
    {
        CollectibleScript.pickUpEvent += SetScore;

        //Pobranie kontrollera UI
        uiController = gameObject.GetComponent<UIController>();
        uiController.winTextGameObject.SetActive(false);
        uiController.gameOverGameObject.SetActive(false);
        uiController.timeToStartText.text = "";

        //Okreœlenie maksymalnej iloœci punktów do zdobycia w ka¿dym poziomie
        collectibles = GameObject.FindGameObjectsWithTag("Collectible");
        maxScore = collectibles.Length;

        //Okreœlenie obecnego numeru sceny
        numberOfScene = SceneManager.GetActiveScene().buildIndex;

        //Pobranie obiektu gracza
        GameObject playerObject = GameObject.FindWithTag("Player");
        if (playerObject != null)
        {
            //Pobranie kontrollera gracza
            movementController = playerObject.GetComponent<MovementController>();
            playerController = playerObject.GetComponent<PlayerController>();

            //Pobranie rigidbody obiektu
            rb = movementController.GetComponent<Rigidbody>();
        }
        else
        {
            Debug.Log("Obiekt Player nie zosta³ znaleziony!");
        }

        //Wyzerowanie punktów gracza w ka¿dym poziomie
        SetNullScore();

        //Wyœwietelenie iloœci ¿yæ gracza
        uiController.lifeText.text = "Life: " + playerController.GetPlayerLife();
    }

    void Update()
    {
        //Sprawdzanie przegranej
        if (playerController.GetPlayerLife() == 0)
        {
            GameOver();
        }

        //Powrót na plansze po wypadniêciu
        if (movementController.transform.position.y < -2)
        {
            movementController.transform.position = CheckpointScript.lastCheckpoint;

            playerController.DecLife();
            uiController.lifeText.text = "Life: " + playerController.GetPlayerLife();

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

            //Odliczanie do nastêpnej rundy
            if (timeStart > 0)
            {
                timeStart -= Time.deltaTime;
                uiController.timeToStartText.text = Mathf.CeilToInt(timeStart).ToString();
            }

            //Za³adowanie nowej sceny
            if (timeStart < 1)
            {
                SceneManager.LoadScene(numberOfScene + 1, LoadSceneMode.Single);
            }
        }
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
        //Odliczanie do nastêpnej rundy
        if (timeStart > 0)
        {
            timeStart -= Time.deltaTime;
            uiController.timeToStartText.text = Mathf.CeilToInt(timeStart).ToString();
        }

        //Za³adowanie koñcowej sceny
        if (timeStart < 1)
        {
            SceneManager.LoadScene("End Scene", LoadSceneMode.Single);
        }
    }

    public void SetNullScore()
    {
        playerScore = 0;
    }

    //Ustawianie wyniku
    public void SetScore()
    {
        playerScore++;
    }

    public int GetScore()
    {
        return playerScore;
    }

    private void OnDisable()
    {
        CollectibleScript.pickUpEvent -= SetScore;
    }
}
