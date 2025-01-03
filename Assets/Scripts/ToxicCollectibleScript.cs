using System;
using UnityEngine;

public class ToxicCollectibleScript : CollectibleScript
{
    //event zebrania ToxicCollectible
    public static event Action toxicPickUpEvent;

    private EnemyScript enemyScript;

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.CompareTag("Player"))
        {
            toxicPickUpEvent?.Invoke();
            GetComponent<ParticleSystem>().Play();
            GetComponent<MeshRenderer>().enabled = false;
            GetComponent<Collider>().enabled = false;

            Light light = GetComponentInChildren<Light>();
            if (light != null)
            {
                StartCoroutine(FadeOutLight(light, 0.5f));
            }
            Invoke(nameof(DeactivateObject), 2.2f);
        }
        else if (collider.gameObject.CompareTag("Enemy"))
        {
            collider.gameObject.GetComponent<ParticleSystem>().Play();
            collider.gameObject.GetComponent<MeshRenderer>().enabled = false;
            collider.gameObject.GetComponent<Collider>().enabled = false;
            collider.gameObject.GetComponent<EnemyScript>().Invoke("DeactivateObject", 3.0f);

            GetComponent<ParticleSystem>().Play();
            GetComponent<MeshRenderer>().enabled = false;
            GetComponent<Collider>().enabled = false;

            Light light = GetComponentInChildren<Light>();
            if (light != null)
            {
                StartCoroutine(FadeOutLight(light, 0.5f));
            }
            Invoke(nameof(DeactivateObject), 2.2f);
        }
    }
}
