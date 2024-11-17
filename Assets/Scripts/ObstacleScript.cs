using UnityEngine;

public class ObstacleScript : MonoBehaviour
{
    //Si�a uderzenia przeszkody
    private float impact = 500f;

    void Update()
    {
        //Rotacja przeszk�d
        transform.Rotate(new Vector3(0, 150, 0) * Time.deltaTime);
    }

    //Kolizja gracza z przeszkod�
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
