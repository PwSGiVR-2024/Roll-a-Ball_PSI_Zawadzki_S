using System;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour
{
    //event uderzenia Playera
    public static event Action onPlayerHit;

    public Transform player;
    private NavMeshAgent navMeshAgent;
    public float knockbackForce = 10f;

    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        if (navMeshAgent == null)
        {
            //Debug.LogError("NavMeshAgent nie zosta³ znaleziony na obiekcie: " + gameObject.name);
            enabled = false;
            return;
        }

        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player")?.transform;
            if (player == null)
            {
                //Debug.LogError("Nie znaleziono obiektu gracza! Upewnij siê, ¿e ma tag 'Player'");
                enabled = false;
                return;
            }
        }
    }

    void Update()
    {
        if (player != null && navMeshAgent != null && navMeshAgent.isOnNavMesh)
        {
            navMeshAgent.SetDestination(player.position);
        }
    }

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
   

}
