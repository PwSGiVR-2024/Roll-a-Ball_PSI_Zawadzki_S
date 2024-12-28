using System;
using System.Collections;
using UnityEngine;

public class ToxicCollectibleScript : MonoBehaviour
{
    public static event Action toxicPickUpEvent;

    private void DeactivateObject()
    {
        //wy³¹czenie punktu
        gameObject.SetActive(false);
    }

    private IEnumerator FadeOutLight(Light light, float duration)
    {
        float startIntensity = light.intensity;
        float time = 0;

        while (time < duration)
        {
            time += Time.deltaTime;
            light.intensity = Mathf.Lerp(startIntensity, 0, time / duration);

            //wznawaia funkcje w nastêpnej klatce
            yield return null;
        }

        light.enabled = false;
    }
    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {

            toxicPickUpEvent?.Invoke();

            GetComponent<ParticleSystem>().Play();

            GetComponent<MeshRenderer>().enabled = false;
            GetComponent<Collider>().enabled = false;

            //wy³¹czenie œwiat³a wraz z powolnym wygaszaniem
            Light light = GetComponentInChildren<Light>();
            if (light != null)
            {
                StartCoroutine(FadeOutLight(light, 0.5f));
            }

            Invoke(nameof(DeactivateObject), 2.2f);
        }
    }

}
