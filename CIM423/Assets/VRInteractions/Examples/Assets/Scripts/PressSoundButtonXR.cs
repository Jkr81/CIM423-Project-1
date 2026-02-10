using System.Collections;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

[RequireComponent(typeof(UnityEngine.XR.Interaction.Toolkit.Interactables.XRBaseInteractable))]
public class PressSoundButtonXR : MonoBehaviour
{
    [Header("Button Visual (the part that moves)")]
    public Transform buttonTop;

    [Tooltip("How deep the button presses (BIG number = deep press)")]
    public float pressDistance = 100f;   // 👈 WAY deeper

    [Tooltip("How fast the button moves")]
    public float pressSpeed = 250f;

    [Header("Sound (Toggle)")]
    public AudioSource audioSource;

    [Tooltip("Optional: sound to play when toggling ON (leave empty if using AudioSource.clip)")]
    public AudioClip pressClip;

    [Tooltip("If true, toggling ON will play AudioSource.clip in a loop. Great for ambient sounds.")]
    public bool loopClip = true;

    [Header("Auto Off")]
    [Tooltip("If true (and loopClip is false), the toggle will automatically reset to OFF when the clip finishes.")]
    public bool autoOffWhenClipEnds = true;

    Vector3 startLocalPos;
    UnityEngine.XR.Interaction.Toolkit.Interactables.XRBaseInteractable interactable;
    Coroutine animRoutine;

    bool isToggledOn = false;
    bool isHeldDown = false;      // prevents spam toggles while still selecting
    bool autoTurnedOff = false;   // prevents repeated auto-off triggers

    void Awake()
    {
        interactable = GetComponent<UnityEngine.XR.Interaction.Toolkit.Interactables.XRBaseInteractable>();

        if (buttonTop != null)
            startLocalPos = buttonTop.localPosition;

        // Safe defaults
        if (audioSource != null)
        {
            audioSource.playOnAwake = false;
            audioSource.loop = loopClip;
        }
    }

    void OnEnable()
    {
        interactable.selectEntered.AddListener(OnPress);
        interactable.selectExited.AddListener(OnRelease);
    }

    void OnDisable()
    {
        interactable.selectEntered.RemoveListener(OnPress);
        interactable.selectExited.RemoveListener(OnRelease);
    }

    void Update()
    {
        if (!autoOffWhenClipEnds) return;
        if (!isToggledOn) return;
        if (audioSource == null) return;
        if (audioSource.loop) return; // only makes sense for non-looping clips

        // If the clip ended naturally, turn the toggle OFF
        if (!audioSource.isPlaying && !autoTurnedOff)
        {
            autoTurnedOff = true;
            isToggledOn = false;
        }
    }

    void OnPress(SelectEnterEventArgs args)
    {
        if (buttonTop == null) return;

        // Block repeated OnPress calls while still held (jitter/spam protection)
        if (isHeldDown) return;
        isHeldDown = true;

        // Toggle sound ON/OFF
        ToggleSound();

        // ⬇ Press INTO the button (rotation-safe)
        Vector3 pressedPos = startLocalPos - buttonTop.up * pressDistance;
        StartAnim(pressedPos);
    }

    void OnRelease(SelectExitEventArgs args)
    {
        if (buttonTop == null) return;

        isHeldDown = false;

        // ⬆ Return to original position
        StartAnim(startLocalPos);
    }

    void ToggleSound()
    {
        if (audioSource == null) return;

        if (!isToggledOn)
        {
            // Turn ON
            isToggledOn = true;
            autoTurnedOff = false;

            // If you provided a pressClip and you want THAT to be the sound, set it as the clip
            if (pressClip != null)
                audioSource.clip = pressClip;

            // Ensure loop behavior matches your toggle style
            audioSource.loop = loopClip;

            // Only play if not already playing (extra spam safety)
            if (!audioSource.isPlaying)
                audioSource.Play();
        }
        else
        {
            // Turn OFF
            isToggledOn = false;
            autoTurnedOff = false;

            // Stop immediately (no layering)
            if (audioSource.isPlaying)
                audioSource.Stop();
        }
    }

    void StartAnim(Vector3 targetLocalPos)
    {
        if (animRoutine != null)
            StopCoroutine(animRoutine);

        animRoutine = StartCoroutine(AnimateTo(targetLocalPos));
    }

    IEnumerator AnimateTo(Vector3 target)
    {
        while (Vector3.Distance(buttonTop.localPosition, target) > 0.0001f)
        {
            buttonTop.localPosition = Vector3.Lerp(
                buttonTop.localPosition,
                target,
                Time.deltaTime * pressSpeed
            );
            yield return null;
        }

        buttonTop.localPosition = target;
    }
}
