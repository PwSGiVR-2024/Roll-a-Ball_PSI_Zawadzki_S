using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    //eventy
    //event w przypadku przegranej
    public event Action gameOverSoundEvent;

    //event w przypadku wygranej
    public event Action gameWinEvent;


    //Player fiyzka
    private MonoBehaviour movementController;

    //Player w³aœciwoœci
    private PlayerController playerController;

    //punkty Playera
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

        //pobranie kontrollera UI
        uiController = gameObject.GetComponent<UIController>();
        uiController.winTextGameObject.SetActive(false);
        uiController.gameOverGameObject.SetActive(false);
        uiController.timeToStartText.text = "";

        //okreœlenie maksymalnej iloœci punktów do zdobycia w ka¿dym poziomie
        collectibles = GameObject.FindGameObjectsWithTag("Collectible");
        maxScore = collectibles.Length;

        //okreœlenie obecnego numeru sceny
        numberOfScene = SceneManager.GetActiveScene().buildIndex;

        //pobranie obiektu Playera
        GameObject playerObject = GameObject.FindWithTag("Player");
        if (playerObject != null)
        {
            if (numberOfScene == 3)
            {
                playerObject.AddComponent<NewMovementController>();
                movementController = playerObject.GetComponent<NewMovementController>();
            }
            else
            {
                playerObject.AddComponent<MovementController>();
                movementController = playerObject.GetComponent<MovementController>();
            }

            //pobranie kontrollera Playera
            playerController = playerObject.GetComponent<PlayerController>();
            playerController.gameOverEvent += GameOver;
        }
        else
        {
            Debug.Log("Obiekt Player nie zosta³ znaleziony!");
        }

        //wyzerowanie punktów Playera w ka¿dym poziomie
        SetNullScore();
        uiController.scoreText.text = "Score: " + playerScore + "/" + GetMaxScore();
        //wyœwietelenie iloœci ¿yæ Playera
        uiController.lifeText.text = "Life: " + playerController.GetPlayerLife();
    }

    private void checkWin()
    {
        if (playerScore >= maxScore)
        {
            gameWinEvent?.Invoke();
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
                countTime = false; //zatrzymanie licznika
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

    //ustawianie wyniku
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

    public int GetMaxScore()
    {
        return maxScore;
    }

    private void OnDisable()
    {
        CollectibleScript.pickUpEvent -= SetScore;
        playerController.gameOverEvent -= GameOver;
    }
}
