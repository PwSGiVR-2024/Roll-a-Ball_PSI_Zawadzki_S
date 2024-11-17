using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuController : MonoBehaviour
{
    public GameObject optionsPanel;
    public GameObject menuPanel;

    public GameObject startButton;
    public GameObject exitButton;
    public GameObject optionsButton;

    public Toggle toggleLayout;

    public void StartGame()
    {
        //Za³adowanie pierwszej sceny
        SceneManager.LoadScene("Level1", LoadSceneMode.Single);
    }

    public void ShowOptions(bool isActive)
    {
        startButton.SetActive(false);
        exitButton.SetActive(false);
        optionsPanel.SetActive(!optionsPanel.activeInHierarchy);

        if (!optionsPanel.activeInHierarchy)
        {
            startButton.SetActive(true);
            exitButton.SetActive(true);
        }
    }

    public void ExitGame()
    {
        SceneManager.LoadScene("End Scene", LoadSceneMode.Single);
        //Zakoñczenie gry
        //Application.Quit();
    }

    public void OnToggleChanged()
    {
        Image imageMenuPanel = menuPanel.GetComponent<Image>();
        Image imageStartButton = startButton.GetComponent<Image>();
        Image imageExitButton = exitButton.GetComponent<Image>();
        Image imageOptionsButton = optionsButton.GetComponent<Image>();
        Image imageToggleLayout = toggleLayout.transform.Find("Background").GetComponent<Image>();
        //Text toggleLayoutText = toggleLayout.GetComponentInChildren<Text>();

        if (toggleLayout.isOn)
        {
            imageMenuPanel.color = new Color(30f / 255f, 55f / 255f, 153f / 255f, 100f / 255);
            imageStartButton.color = new Color(250f / 255f, 211f / 255f, 144f / 255f, 1f);
            imageExitButton.color = new Color(229f / 255f, 80f / 255f, 57f / 255f, 1f);
            imageOptionsButton.color = new Color(184f / 255f, 233f / 255f, 148f / 255f, 1f);
            imageToggleLayout.color = new Color(130f / 255f, 204f / 255f, 221f / 255f);
        }
        else
        {
            imageMenuPanel.color = new Color(30f / 255f, 55f / 255f, 153f / 255f, 150f / 255);
            imageStartButton.color = new Color(246f / 255f, 185f / 255f, 59f / 255f, 1f);
            imageExitButton.color = new Color(235f / 255f, 47f / 255f, 6f / 255f, 1f);
            imageOptionsButton.color = new Color(120f / 255f, 224f / 255f, 143f / 255f, 1f);
            imageToggleLayout.color = new Color(60f / 255f, 99f / 255f, 130f / 255f);
        }
    }
}
