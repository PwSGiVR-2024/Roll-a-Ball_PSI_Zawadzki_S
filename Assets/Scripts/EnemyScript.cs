using System;
using UnityEngine;
public class EnemyScript : MonoBehaviour
{
    //event uderzenia Playera
    public static event Action onPlayerHit;

    private GameController gameController;
    private void Start()
    {
        gameController = FindFirstObjectByType<GameController>();
        gameController.gameOverSoundEvent += DeactivateAndPlayParticleSystem;
    }

    public float knockbackForce = 10f;
    private void OnCollisionEnter(Collision other)
    {
        Debug.Log("Uderzenie z graczem");
        if (other.gameObject.CompareTag("Player"))
        {
            NewMovementController newMovementController = other.gameObject.GetComponent<NewMovementController>();
            PlayerController playerController = other.gameObject.GetComponent<PlayerController>();

            if (newMovementController != null && playerController != null)
            {
                Vector3 knockbackDirection = (other.transform.position - transform.position).normalized;
                newMovementController.ApplyKnockback(knockbackDirection, knockbackForce);

                onPlayerHit?.Invoke();
            }
        }
    }
    public void DeactivateObject()
    {
        //wy³¹czenie Collectible
        gameObject.SetActive(false);
    }

    public void DeactivateAndPlayParticleSystem()
    {
        GetComponent<ParticleSystem>().Play();

        GetComponent<MeshRenderer>().enabled = false;
        GetComponent<Collider>().enabled = false;
        Invoke("DeactivateObject", 3.0f);
    }

    private void OnDisable()
    {
        gameController.gameOverSoundEvent -= DeactivateAndPlayParticleSystem;
    }
}
