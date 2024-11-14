using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Collectible : MonoBehaviour
{

    void Update()
    {
        //Rotacja punkt�w
        transform.Rotate(new Vector3(30,20,10) * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider collision)
    {
        //Inkrementacja punkt�w gracza
        collision.gameObject.GetComponent<MovementController>().SetScore();

        //Wy��czenie punktu
        gameObject.SetActive(false);
    }
}
