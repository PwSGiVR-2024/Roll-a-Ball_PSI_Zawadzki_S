using UnityEngine;

public class ObstacleScript : MonoBehaviour
{
    //si³a uderzenia przeszkody
    private float impact = 500f;

    void Update()
    {
        //rotacja przeszkód
        transform.Rotate(new Vector3(0, 150f, 0) * Time.deltaTime);
    }

    //kolizja gracza z przeszkod¹
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

        if (collision.gameObject.CompareTag("Enemy"))
        {
            Rigidbody enemyRb = collision.gameObject.GetComponent<Rigidbody>();
            if (enemyRb != null)
            {
                Vector3 impactDirection = collision.transform.position - transform.position;
                impactDirection.y = 0;
                enemyRb.AddForce(impactDirection * impact, ForceMode.Force);
            }
        }
    }
}
