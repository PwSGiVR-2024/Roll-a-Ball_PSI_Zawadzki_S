using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CollectibleScript : MonoBehaviour
{
    //public event Action collectibleSoundEvent;
    public static event Action pickUpEvent;

    //public AudioSource pointAudioSource;

    //private GameController gameController;
    //private PlayerController playerController;
    //private bool isCollected = false;

    void Start()
    {
        //pointAudioSource = FindFirstObjectByType<AudioSource>();

        //gameController = FindFirstObjectByType<GameController>();
        //playerController = FindFirstObjectByType<PlayerController>();
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
        if (collision.gameObject.CompareTag("Player"))
        {

            //W��czenie d�wi�ku zbierania
            //collectibleSoundEvent?.Invoke();
            pickUpEvent?.Invoke();

            GetComponent<MeshRenderer>().enabled = false;
            GetComponent<Collider>().enabled = false;

            //Inkrementacja punkt�w gracza
            //playerController.SetScore();

            Invoke(nameof(DeactivateObject), 2.2f);
        }
    }
}
