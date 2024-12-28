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


    private KillBoxScript killBox;

    void Start()
    {
        gameController = FindFirstObjectByType<GameController>();
        playerController = FindFirstObjectByType<PlayerController>();

        killBox = FindFirstObjectByType<KillBoxScript>();
        if (killBox != null)
        {
            killBox.OnKill += updatePlayerLife;

        }

        ToxicCollectibleScript.toxicPickUpEvent += updatePlayerLife;
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
        if (FindAnyObjectByType<KillBoxScript>() != null)
        {
            FindAnyObjectByType<KillBoxScript>().OnKill -= updatePlayerLife;
        }

        ToxicCollectibleScript.toxicPickUpEvent -= updatePlayerLife;
        killBox.OnKill -= updatePlayerLife;
    }
}
   
