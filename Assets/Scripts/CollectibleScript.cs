using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CollectibleScript : MonoBehaviour
{
    public event Action collectibleSoundEvent;

    //public AudioSource pointAudioSource;

    private GameController gameController;
    private bool isCollected = false;

    void Start()
    {
        //pointAudioSource = FindFirstObjectByType<AudioSource>();

        gameController = FindFirstObjectByType<GameController>();
    }
    void Update()
    {
        //Rotacja punkt�w
        transform.Rotate(new Vector3(30,20,10) * Time.deltaTime);
    }

    private void DeactivateObject()
    {
        //Wy��czenie punktu
        gameObject.SetActive(false);

    }
    private void OnTriggerEnter(Collider collision)
    {
        if (isCollected) { return; } 

        isCollected = true;

        //W��czenie d�wi�ku zbierania
        collectibleSoundEvent?.Invoke();
        //pointAudioSource.Play();

        gameObject.GetComponent<MeshRenderer>().enabled = false;

        //Inkrementacja punkt�w gracza
        gameController.SetScore();

        Invoke(nameof(DeactivateObject), 2.2f);
    }
}
