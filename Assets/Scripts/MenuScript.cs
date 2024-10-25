using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuScript : MonoBehaviour
{
    public GameObject optionsPanel;


    public void StartGame()
    {
        Debug.Log("Start gry");
        SceneManager.LoadScene("Level1", LoadSceneMode.Single);
    }

    public void ShowOptions(bool isActive)
    {
        Debug.Log("Poka¿ opcje gry");
        optionsPanel.SetActive(!optionsPanel.activeInHierarchy);

    }
    public void CloseOptions()
    {
        Debug.Log("Zamknij opcje gry");
        optionsPanel.SetActive(false);

    }

    public void ExitGame()
    {
        Debug.Log("Koniec gry");
        Application.Quit();
    }


    

}
