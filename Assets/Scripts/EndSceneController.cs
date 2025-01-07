using System;
using UnityEngine;
using UnityEngine.UI;

public class EndSceneController : MonoBehaviour
{
    public event Action endMusicSoundEvent;

    [SerializeField]
    private Text thanksText;

    private void Start()
    {
        endMusicSoundEvent?.Invoke();

        if (PlayerNameController.Instance != null && !string.IsNullOrEmpty(PlayerNameController.Instance.playerName))
        {
            thanksText.text = "Thank you " + PlayerNameController.Instance.playerName + ", for playing the game!";
        }
        else
        {
            thanksText.text = "Thank you for playing the game!";
        }
    }

    public void ExitGame()
    {
        //Debug.Log("Koniec Gry");
        Application.Quit();
    }
}
