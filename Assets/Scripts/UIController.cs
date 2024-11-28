using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    //Canvas
    public Text scoreText;
    public GameObject winTextGameObject;
    public GameObject gameOverGameObject;
    public Text timeToStartText;
    public Text lifeText;

    private GameController gameController;
    private PlayerController playerController;


    void Start()
    {
        gameController = FindFirstObjectByType<GameController>();
        playerController = FindAnyObjectByType<PlayerController>();

        FindAnyObjectByType<KillBoxScript>().OnKill += updatePlayerLife;

        CollectibleScript.pickUpEvent += updatePlayerScore;
    }

    public void updatePlayerScore()
    {
        scoreText.text = "Score: " + gameController.GetScore();
    }

    public void updatePlayerLife()
    {
        lifeText.text = "Life: " + playerController.GetPlayerLife();
    }

    private void OnDisable()
    {
        CollectibleScript.pickUpEvent -= updatePlayerScore;
    }
}
