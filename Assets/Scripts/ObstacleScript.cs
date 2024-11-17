using UnityEngine;

public class ObstacleScript : MonoBehaviour
{
    //Si³a uderzenia przeszkody
    private float impact = 500f;

    void Update()
    {
        //Rotacja przeszkód
        transform.Rotate(new Vector3(0, 150, 0) * Time.deltaTime);
    }

    //Kolizja gracza z przeszkod¹
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Rigidbody playerRb = collision.gameObject.GetComponent<Rigidbody>();

            if (playerRb != null)
            {
                Vector3 impactDirection = collision.transform.position - transform.position;
                impactDirection.y = 0;

                playerRb.AddForce(impactDirection * impact, ForceMode.Force);
            }
        }
    }
}
