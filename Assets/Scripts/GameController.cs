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

    //Gracz fiyzka
    private MonoBehaviour movementController;

    //Gracz w³aœciwoœci
    private PlayerController playerController;

    //punkty gracza
    private int playerScore = 0;

    //maksymalna liczba puntków do zdobycia
    private GameObject[] collectibles;
    private int maxScore;

    //numer sceny
    private int numberOfScene;
    
    private UIController uiController;

    //timer
    private bool countTime = false;
    private float timer = 3;
    private int sceneToLoad = 0;

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
            if (numberOfScene == 3)
            {
                Debug.Log("Nowy Movement Controller");
                playerObject.AddComponent<NewMovementController>();
                movementController = playerObject.GetComponent<NewMovementController>();
            }
            else
            {
                Debug.Log("Stary Movement Controller");
                playerObject.AddComponent<MovementController>();
                movementController = playerObject.GetComponent<MovementController>();
            }
            //Pobranie kontrollera gracza
            playerController = playerObject.GetComponent<PlayerController>();
            playerController.gameOverEvent += GameOver;
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

    private void checkWin()
    {
        if (playerScore >= maxScore)
        {
            uiController.winTextGameObject.SetActive(true);

            if (movementController is NewMovementController newController)
            {
                newController.DisableRigidbody();
            }
            else if (movementController is MovementController oldController)
            {
                oldController.DisableRigidbody();
            }

            StartCountdown(numberOfScene + 1);
        }
    }

    private void Update()
    {
        if (countTime)
        {
            timer -= Time.deltaTime;
            uiController.timeToStartText.text = Mathf.CeilToInt(timer).ToString();

            if (timer <= 0)
            {
                countTime = false; //Zatrzymanie licznika
                uiController.timeToStartText.text = "Loading...";

                //opóŸnienie dla muzyki
                Invoke(nameof(LoadNextScene), 1f);
            }
        }
    }

    public void GameOver()
    {
        gameOverSoundEvent?.Invoke();

        if (movementController is NewMovementController newController)
        {
            newController.DisableRigidbody();
        }
        else if (movementController is MovementController oldController)
        {
            oldController.DisableRigidbody();
        }

        uiController.gameOverGameObject.SetActive(true);

        StartCountdown(4);
    }
    
    private void LoadNextScene()
    {
        SceneManager.LoadScene(sceneToLoad, LoadSceneMode.Single);
    }

    private void StartCountdown(int targerScene)
    {
        sceneToLoad = targerScene;
        timer = 3f;
        countTime = true;
    }

    public void SetNullScore()
    {
        playerScore = 0;
    }

    //Ustawianie wyniku
    public void SetScore()
    {
        playerScore++;
        uiController.updatePlayerScore();
        checkWin();
    }

    public int GetScore()
    {
        return playerScore;
    }
    private void OnDisable()
    {
        CollectibleScript.pickUpEvent -= SetScore;
        playerController.gameOverEvent -= GameOver;
    }
}
