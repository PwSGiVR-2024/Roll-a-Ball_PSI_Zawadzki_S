using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Collectible : MonoBehaviour
{

    void Update()
    {
        transform.Rotate(new Vector3(30,20,10) * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider collision)
    {
        collision.gameObject.GetComponent<MovementController>().SetScore();
       
        gameObject.SetActive(false);
    }
}
