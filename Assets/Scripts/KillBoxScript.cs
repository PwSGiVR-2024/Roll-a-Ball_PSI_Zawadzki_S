using System;
using UnityEngine;

public class KillBoxScript : MonoBehaviour
{

    public event Action OnKill;

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            OnKill?.Invoke();
        }
    }
}
