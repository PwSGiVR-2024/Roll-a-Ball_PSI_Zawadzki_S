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

    void Start()
    {
        gameController = FindFirstObjectByType<GameController>();

        gameController.pickUpEvent += updatePlayerScore;
    }
    public void updatePlayerScore()
    {
        scoreText.text = "Score: " + gameController.GetScore();
    }
}
