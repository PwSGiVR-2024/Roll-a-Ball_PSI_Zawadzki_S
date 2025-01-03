using System;
using UnityEngine;

public class EndSceneController : MonoBehaviour
{
    public event Action endMusicSoundEvent;
    private void Start()
    {
        endMusicSoundEvent?.Invoke();
    }

    public void ExitGame()
    {
        //Debug.Log("Koniec Gry");
        Application.Quit();
    }
}
