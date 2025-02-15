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

    //Obiekty
    private GameController gameController;
    private PlayerController playerController;
    private KillBoxScript killBox;

    void Start()
    {
        gameController = FindFirstObjectByType<GameController>();
        playerController = FindFirstObjectByType<PlayerController>();

        //Przypisanie event�w
        killBox = FindFirstObjectByType<KillBoxScript>();
        if (killBox != null)
        {
            killBox.OnKill += updatePlayerLife;

        }
        ToxicCollectibleScript.toxicPickUpEvent += updatePlayerLife;
        LifeCollectibleScript.lifePickUpEvent += updatePlayerLife;
        EnemyScript.onPlayerHit += updatePlayerLife;
    }

    //aktualizacja punkt�w Playera
    public void updatePlayerScore()
    {
        scoreText.text = "Score: " + gameController.GetScore() + "/" + gameController.GetMaxScore();
    }

    //aktualizacja �ycia Playera
    public void updatePlayerLife()
    {
        lifeText.text = "Life: " + playerController.GetPlayerLife();
    }

    private void OnDisable()
    {
        EnemyScript.onPlayerHit -= updatePlayerLife;
        ToxicCollectibleScript.toxicPickUpEvent -= updatePlayerLife;
        LifeCollectibleScript.lifePickUpEvent -= updatePlayerLife;

        if (killBox != null)
        {
            killBox.OnKill -= updatePlayerLife;
        }
    }
}
   
