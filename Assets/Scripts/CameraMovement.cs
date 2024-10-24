using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour
{
    public GameObject player;

    [SerializeField]
    private Vector3 offset = new Vector3(0, 10, -5);

    void Update()
    {
        gameObject.transform.position = player.transform.position + offset;
    }
}
