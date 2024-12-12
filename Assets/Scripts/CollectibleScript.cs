using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CollectibleScript : MonoBehaviour
{
    public static event Action pickUpEvent;

    public float height = 0.5f; //wysokoœæ podskoku
    public float frequency = 1.2f; //czêstotliwoœæ podskoków
    private Vector3 firstPosition;

    private void Start()
    {
        firstPosition = transform.position;
    }
    void Update()
    {
        /*float offsetY = Mathf.Abs(Mathf.Sin(Time.time * frequency)) * height;
        transform.position = firstPosition + new Vector3(0, offsetY, 0);*/
    }

    private void DeactivateObject()
    {
        //Wy³¹czenie punktu
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
            pickUpEvent?.Invoke();
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
