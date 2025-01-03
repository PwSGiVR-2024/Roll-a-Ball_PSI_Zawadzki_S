using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class ChromaticEffectManager : MonoBehaviour
{
    //instancja singletona
    public static ChromaticEffectManager Instance { get; private set; }

    private PostProcessVolume volume;
    private ChromaticAberration chromaticAberration;

    [SerializeField] private float intensityIncreaseAmount = 0.1f;
    private float maxIntensity = 1.0f;
    private float intensity = 0.0f;

    private void Awake()
    {
        //ustawienie singletona
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        //pobranie Volume
        volume = GetComponent<PostProcessVolume>();

        if (volume != null && volume.profile != null)
        {
            chromaticAberration = volume.profile.GetSetting<ChromaticAberration>();

            if (chromaticAberration == null)
            {
                Debug.LogError("Nie znaleziono efektu Chromatic Aberration w profilu!");
            }
        }
    }

    public void IncreaseIntensity()
    {
        if (chromaticAberration != null)
        {
            if(intensity < maxIntensity)
            {
                intensity += intensityIncreaseAmount;
                chromaticAberration.intensity.value = intensity;
                //Debug.Log($"Nowa intensywnoœæ: {intensity}");
            }
        }
    }
}