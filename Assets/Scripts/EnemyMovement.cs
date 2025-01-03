using System;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour
{
    public Transform player;
    private NavMeshAgent navMeshAgent;
    
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
}
