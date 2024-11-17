using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioSource collectibleAudioSource;
    private GameObject[] collectibles;
    void Start()
    {
        collectibles = GameObject.FindGameObjectsWithTag("Collectible");
        foreach (var collectible in collectibles)
        {
            collectible.GetComponent<CollectibleScript>().collectibleSoundEvent += playCollectibleSound;
        }
    }

    private void playCollectibleSound()
    {
        collectibleAudioSource.Play();
    }
}
