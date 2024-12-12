using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CollectibleScript : MonoBehaviour
{
    public static event Action pickUpEvent;

    public float height = 0.5f; //wysoko�� podskoku
    public float frequency = 1.2f; //cz�stotliwo�� podskok�w
    private Vector3 firstPosition;

    private void Start()
    {
        firstPosition = transform.position;
    }
    void Update()
    {
        /*float offsetY = Mathf.Abs(Mathf.Sin(Time.time * frequency)) * height;
        transform.position = firstPosition + new Vector3(0, offsetY, 0);*/
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
            pickUpEvent?.Invoke();
            //GetComponent<ParticleSystem>().Play();

            GetComponent<MeshRenderer>().enabled = false;
            GetComponent<Collider>().enabled = false;

            Invoke(nameof(DeactivateObject), 2.2f);
        }
    }
}
