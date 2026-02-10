using System.Collections;
using UnityEngine;

public class WaterStepSoundXR : MonoBehaviour
{
    [Header("Audio")]
    public AudioSource waterAudio;

    [Tooltip("Target volume when on water")]
    [Range(0f, 1f)]
    public float onWaterVolume = 1f;

    [Tooltip("Seconds to fade in/out")]
    public float fadeDuration = 1.0f;

    [Header("Debug")]
    public bool logTriggers = false;

    Coroutine fadeRoutine;
    bool cameraInside = false;

    void Awake()
    {
        if (waterAudio != null)
        {
            waterAudio.playOnAwake = false;
            waterAudio.volume = 0f;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // Detect MAIN CAMERA ONLY
        if (!other.CompareTag("MainCamera")) return;

        if (cameraInside) return;
        cameraInside = true;

        if (logTriggers)
            Debug.Log("[WaterStepSoundXR] Camera ENTER water", this);

        StartFade(true);
    }

    private void OnTriggerExit(Collider other)
    {
        if (!other.CompareTag("MainCamera")) return;

        cameraInside = false;

        if (logTriggers)
            Debug.Log("[WaterStepSoundXR] Camera EXIT water", this);

        StartFade(false);
    }

    void StartFade(bool fadeIn)
    {
        if (waterAudio == null) return;

        if (fadeRoutine != null)
            StopCoroutine(fadeRoutine);

        fadeRoutine = StartCoroutine(Fade(fadeIn));
    }

    IEnumerator Fade(bool fadeIn)
    {
        float start = waterAudio.volume;
        float end = fadeIn ? onWaterVolume : 0f;

        if (fadeIn && !waterAudio.isPlaying)
            waterAudio.Play();

        float t = 0f;
        while (t < fadeDuration)
        {
            waterAudio.volume = Mathf.Lerp(start, end, t / fadeDuration);
            t += Time.deltaTime;
            yield return null;
        }

        waterAudio.volume = end;

        if (!fadeIn)
            waterAudio.Stop();
    }
}
