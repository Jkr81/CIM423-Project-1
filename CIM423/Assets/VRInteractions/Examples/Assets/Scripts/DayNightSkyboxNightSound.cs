using UnityEngine;

public class DayNightSkyboxNightSound : MonoBehaviour
{
    [Header("Skyboxes")]
    public Material daySkybox;
    public Material nightSkybox;

    [Header("Night Audio Only")]
    public AudioSource nightAudio;

    [Header("Optional Lighting")]
    public Light directionalLight;
    public float dayLightIntensity = 1f;
    public float nightLightIntensity = 0.25f;

    bool isNight = false;

    void Start()
    {
        SetDay();
    }

    public void ToggleDayNight()
    {
        if (isNight) SetDay();
        else SetNight();
    }

    void SetNight()
    {
        isNight = true;

        RenderSettings.skybox = nightSkybox;
        DynamicGI.UpdateEnvironment();

        if (directionalLight)
            directionalLight.intensity = nightLightIntensity;

        if (nightAudio)
        {
            nightAudio.loop = true;
            if (!nightAudio.isPlaying)
                nightAudio.Play();
        }
    }

    void SetDay()
    {
        isNight = false;

        RenderSettings.skybox = daySkybox;
        DynamicGI.UpdateEnvironment();

        if (directionalLight)
            directionalLight.intensity = dayLightIntensity;

        if (nightAudio && nightAudio.isPlaying)
            nightAudio.Stop();
    }
}
