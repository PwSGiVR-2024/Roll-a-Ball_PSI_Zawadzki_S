using UnityEngine;
using UnityEngine.UI;

public class JumpTextScript : MonoBehaviour
{
    public GameObject jumpText;

    GameController gameController;
    void Start()
    {
        gameController = FindAnyObjectByType<GameController>();

        if (jumpText != null && jumpText != null)
        {
            jumpText.SetActive(false);
        }
    }

    void Update()
    {
        if (gameController.GetScore() == 4)
        {
            jumpText.SetActive(true);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        jumpText.SetActive(false);
        gameObject.SetActive(false);
    }
}
