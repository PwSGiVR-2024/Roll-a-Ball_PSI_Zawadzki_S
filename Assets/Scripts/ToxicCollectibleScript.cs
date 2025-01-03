using System;
using UnityEngine;

public class ToxicCollectibleScript : CollectibleScript
{
    //event zebrania ToxicCollectible
    public static event Action toxicPickUpEvent;

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
            //bezpoœrednie wy³¹czenie konkretnego Enemy
            collider.gameObject.SetActive(false);

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
