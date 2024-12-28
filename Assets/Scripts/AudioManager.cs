using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioSource collectibleAudioSource;
    public AudioSource toxicCollectibleAudioSource;

    private GameObject gameControllerObject;
    public AudioSource musicBgAudioSource;
    public AudioSource gameOverAudioSource;

    void Start()
    {
        CollectibleScript.pickUpEvent += PlayCollectibleSound;
        ToxicCollectibleScript.toxicPickUpEvent += PlayToxicCollectibleSound;

        //FindAnyObjectByType<KillBoxScript>().OnKill += PlayToxicCollectibleSound;


        gameControllerObject = GameObject.FindGameObjectWithTag("GameController");
        gameControllerObject.GetComponent<GameController>().gameOverSoundEvent += PlayGameOverSound;

        gameControllerObject.GetComponent<GameController>().bgMusicSoundEvent += PlayBgSound;
    }

    private void PlayToxicCollectibleSound()
    {
        if (toxicCollectibleAudioSource != null)
        {
            toxicCollectibleAudioSource.Play();
        }
        else 
        {
            Debug.LogWarning("Toxic Collectible AudioSource is missing!");
        }
    }

    private void PlayCollectibleSound()
    {
        if (collectibleAudioSource != null)
        {
            collectibleAudioSource.Play();

        }
        else
        {
            Debug.LogWarning("Collectible AudioSource is missing!");
        }
    }

    private void PlayBgSound()
    {
        if (musicBgAudioSource != null)
        {
            musicBgAudioSource.Play();

        }
        else
        {
            Debug.LogWarning("Background Music AudioSource is missing!");
        }
    }

    private void PlayGameOverSound()
    {
        if (musicBgAudioSource != null)
        {
            musicBgAudioSource.Stop();

        }

        if (gameOverAudioSource != null)
        {
            gameOverAudioSource.Play();

        }
        else
        {
            Debug.LogWarning("Game Over AudioSource is missing!");

        }
    }

    private void OnDisable()
    {
        CollectibleScript.pickUpEvent -= PlayCollectibleSound;
        ToxicCollectibleScript.toxicPickUpEvent -= PlayToxicCollectibleSound;

        if (FindAnyObjectByType<KillBoxScript>() != null)
            FindAnyObjectByType<KillBoxScript>().OnKill -= PlayToxicCollectibleSound;

        if (gameControllerObject != null)
        {
            var gameController = gameControllerObject.GetComponent<GameController>();
            if (gameController != null)
            {
                gameController.gameOverSoundEvent -= PlayGameOverSound;
                gameController.bgMusicSoundEvent -= PlayBgSound;
            }
        }
    }
    
}  
