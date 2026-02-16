using UnityEngine;

public class RegisterInteractionButton : MonoBehaviour
{
    [Tooltip("Drag in the GameManager that has SensoryProgress.")]
    public SensoryProgress progress;

    [Tooltip("Unique ID for this button (ex: Button1, SoundButton_A, etc).")]
    public string buttonId = "Button1";

    [Header("One-time press")]
    public bool onlyCountOnce = true;

    private bool alreadyCounted = false;

    // Call this from your XR button event (Select Entered / Activated / OnPress)
    public void OnPressed()
    {
        if (progress == null) return;

        if (onlyCountOnce && alreadyCounted) return;
        alreadyCounted = true;

        progress.RegisterPress(buttonId);
    }
}
