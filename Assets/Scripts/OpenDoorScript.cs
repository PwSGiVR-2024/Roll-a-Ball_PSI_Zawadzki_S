using UnityEngine;

public class OpenDoorScript : MonoBehaviour
{
    private GameController gameController;

    void Start()
    {
        gameController = FindFirstObjectByType<GameController>();
    }

    void Update()
    {
        if(gameController.GetScore() == 4)
        {
            gameObject.SetActive(false);
        }
    }
}
