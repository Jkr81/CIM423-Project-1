using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitDoorButton : MonoBehaviour
{
    [Header("Progress")]
    public SensoryProgress progress;

    [Header("Audio (Plays ONLY if not complete)")]
    public AudioSource instructionAudio;

    [Tooltip("If true, restarting reloads the current scene.")]
    public bool reloadCurrentScene = true;

    [Header("Safety")]
    public float pressCooldown = 1.0f;
    private float lastPressTime = -999f;

    // Call this from your Exit button XR event
    public void OnExitPressed()
    {
        if (Time.time - lastPressTime < pressCooldown) return;
        lastPressTime = Time.time;

        if (progress == null)
        {
            Debug.LogWarning("ExitDoorButton: progress not assigned.");
            return;
        }

        // If they’re NOT done → play instruction audio (once, no stacking)
        if (!progress.IsComplete)
        {
            if (instructionAudio == null) return;

            if (!instructionAudio.isPlaying)
            {
                instructionAudio.Play();
            }
            return;
        }

        // If they ARE done → restart scene
        if (reloadCurrentScene)
        {
            Scene current = SceneManager.GetActiveScene();
            SceneManager.LoadScene(current.buildIndex);
        }
    }
}
