using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class PauseMenuController : MonoBehaviour
{
    [SerializeField] private GameObject pauseMenuUI;
    [SerializeField] private Image darkBackground;
    [SerializeField] private float fadeDuration = 0.3f; //czas trwania animacji przyciemnienia
    [SerializeField] private float maxAlpha = 0.7f; //maksymalna przezroczystoœæ przyciemnienia (0-1)

    public static bool isPaused = false;
    private static PauseMenuController instance;
    private float currentFadeTime = 0f;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;

            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    void Start()
    {
        pauseMenuUI.SetActive(false);

        //pocz¹tkowa przezroczystoœæ t³a
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
            if (Input.GetKeyDown(KeyCode.Escape))
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

        //animacja przyciemnienia
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
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;
    }

    void Pause()
    {
        currentFadeTime = 0f;
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;
    }

    bool IsGameplayScene()
    {
        string[] nonGameplayScenes = { "Main Menu", "End Scene"};
        string currentScene = SceneManager.GetActiveScene().name;

        foreach (string sceneName in nonGameplayScenes)
        {
            if (currentScene == sceneName) return false;
        }
        return true;
    }

    public void QuitGame()
    {
        if (darkBackground != null)
        {
            darkBackground.gameObject.SetActive(false);
        }
        if (pauseMenuUI != null)
        {
            pauseMenuUI.SetActive(false);
        }

        Time.timeScale = 1f;
        isPaused = false;
        Destroy(gameObject);

        SceneManager.LoadScene("End Scene");
    }

    public static bool IsPaused()
    {
        return isPaused;
    }
}