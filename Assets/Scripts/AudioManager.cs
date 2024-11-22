using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioSource collectibleAudioSource;

    private GameObject gameControllerObject;
    public AudioSource musicBgAudioSource;
    public AudioSource gameOverAudioSource;

    void Start()
    {
        CollectibleScript.pickUpEvent += PlayCollectibleSound;

        gameControllerObject = GameObject.FindGameObjectWithTag("GameController");
        gameControllerObject.GetComponent<GameController>().gameOverSoundEvent += PlayGameOverSound;

        gameControllerObject.GetComponent<GameController>().bgMusicSoundEvent += PlayBgSound;
    }

    private void PlayBgSound()
    {
        musicBgAudioSource.Play();
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

    private void OnDisable()
    {
        CollectibleScript.pickUpEvent -= PlayCollectibleSound;
    }

}
