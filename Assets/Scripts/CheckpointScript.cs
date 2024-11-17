using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointScript : MonoBehaviour
{
    //Checkpoint i jego vector
    static public Vector3 lastCheckpoint;
  
    void Update()
    {
        //Rotacja checkpointu
        transform.Rotate(new Vector3(30, 20, 10) * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider collision)
    {
        lastCheckpoint = gameObject.transform.position;
        //Wy³¹czenie checkpointa
        gameObject.SetActive(false);
    }
}
