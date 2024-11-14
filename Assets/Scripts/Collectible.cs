using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Collectible : MonoBehaviour
{

    void Update()
    {
        //Rotacja punktów
        transform.Rotate(new Vector3(30,20,10) * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider collision)
    {
        //Inkrementacja punktów gracza
        collision.gameObject.GetComponent<MovementController>().SetScore();

        //Wy³¹czenie punktu
        gameObject.SetActive(false);
    }
}
