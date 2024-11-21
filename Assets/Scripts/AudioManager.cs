using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioSource collectibleAudioSource;
    private GameObject[] collectibles;

    private GameObject gameControllerObject;
    public AudioSource musicBgAudioSource;
    public AudioSource gameOverAudioSource;

    void Start()
    {
        collectibles = GameObject.FindGameObjectsWithTag("Collectible");
        foreach (var collectible in collectibles)
        {
            collectible.GetComponent<CollectibleScript>().collectibleSoundEvent += PlayCollectibleSound;
        }

        gameControllerObject = GameObject.FindGameObjectWithTag("GameController");
        gameControllerObject.GetComponent<GameController>().gameOverSoundEvent += PlayGameOverSound;
        gameControllerObject.GetComponent<GameController>().bgSoundEventStop += StopBgSound;

        gameControllerObject.GetComponent<GameController>().bgMusicSoundEvent += PlayBgSound;
    }

    private void PlayBgSound()
    {
        musicBgAudioSource.Play();
    }

    private void StopBgSound()
    {
        
    }

    private void PlayCollectibleSound()
    {
        collectibleAudioSource.Play();
    }

    private void PlayGameOverSound()
    {
        musicBgAudioSource.Stop();
        gameOverAudioSource.Play();
    }

}
