using UnityEngine;

public class EndSceneController : MonoBehaviour
{
    public void ExitGame()
    {
        Debug.Log("Koniec Gry");
        Application.Quit();
    }
}
