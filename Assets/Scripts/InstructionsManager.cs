using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class InstructionsManager : MonoBehaviour
{
    //menu instrukcji
    [SerializeField] private GameObject instructionsCanvas;
    [SerializeField] private Image darkBackground;
    [SerializeField] private float fadeDuration = 0.3f;
    [SerializeField] private float maxAlpha = 0.7f;

    public static bool isPaused = false;
    private float currentFadeTime = 0f;

    void Start()
    {
        instructionsCanvas.SetActive(false);

        // Pocz¹tkowa przezroczystoœæ t³a
        Color bgColor = darkBackground.color;
        bgColor.a = 0f;
        darkBackground.color = bgColor;

        Time.timeScale = 1f;
        isPaused = false;
    }

    void Update()
    {
        if (IsGameplayScene())
        {
            if (Input.GetKeyDown(KeyCode.I))
            {
                if (isPaused)
                {
                    Resume();
                }
                else
                {
                    Pause();
                }
            }
        }

        if (isPaused && darkBackground.color.a < maxAlpha)
        {
            FadeIn();
        }
        else if (!isPaused && darkBackground.color.a > 0)
        {
            FadeOut();
        }
    }

    void FadeIn()
    {
        Color bgColor = darkBackground.color;
        bgColor.a = Mathf.Lerp(0f, maxAlpha, currentFadeTime / fadeDuration);
        darkBackground.color = bgColor;
        currentFadeTime += Time.unscaledDeltaTime;
    }

    void FadeOut()
    {
        Color bgColor = darkBackground.color;
        bgColor.a = Mathf.Lerp(maxAlpha, 0f, currentFadeTime / fadeDuration);
        darkBackground.color = bgColor;
        currentFadeTime += Time.unscaledDeltaTime;
    }

    public void Resume()
    {
        currentFadeTime = 0f;
        instructionsCanvas.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;
    }

    void Pause()
    {
        currentFadeTime = 0f;
        instructionsCanvas.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;
    }

    public void ShowInstructions()
    {
        instructionsCanvas.SetActive(true);
    }

    public void HideInstructions()
    {
        instructionsCanvas.SetActive(false);
    }

    bool IsGameplayScene()
    {
        string[] nonGameplayScenes = { "Main Menu", "End Scene" };
        string currentScene = SceneManager.GetActiveScene().name;

        foreach (string sceneName in nonGameplayScenes)
        {
            if (currentScene == sceneName) return false;
        }
        return true;
    }

    public static bool IsPaused()
    {
        return isPaused;
    }
}
