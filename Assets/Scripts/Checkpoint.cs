using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    void Update()
    {
        //Rotacja checkpointu
        transform.Rotate(new Vector3(30, 20, 10) * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider collision)
    {
        collision.gameObject.GetComponent<MovementController>();

        //Znalezienie klasy level2Controller i ustawienie nowej pozycji po uzyskaniu checkpointa
        Level2_Controller level2Controller = FindObjectOfType<Level2_Controller>();
        level2Controller.SetVector3(0f, 0.5f, 28.035f);

        //Wy³¹czenie checkpointa
        gameObject.SetActive(false);
    }
}
