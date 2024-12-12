using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovementScript : MonoBehaviour
{
    public GameObject player;

    [SerializeField]
    private Vector3 offset = new Vector3(0, 5.5f, -3);

    void Update()
    {
        //Ustawienie kamery z offsetem za graczem
        gameObject.transform.position = player.transform.position + offset;
    }
}