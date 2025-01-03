using UnityEngine;
using System;

public class LifeCollectibleScript : CollectibleScript
{
    //event zebrania LifeCollectible
    public static event Action lifePickUpEvent;

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.CompareTag("Player"))
        {
            lifePickUpEvent?.Invoke();
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
