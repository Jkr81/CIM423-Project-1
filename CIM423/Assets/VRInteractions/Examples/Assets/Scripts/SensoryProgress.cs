using System.Collections.Generic;
using UnityEngine;

public class SensoryProgress : MonoBehaviour
{
    [Header("Goal")]
    public int totalButtonsRequired = 7;

    [Header("Debug")]
    public bool logProgress = true;

    // Tracks unique buttons so you can’t spam one button to reach 7
    private HashSet<string> pressedButtonIds = new HashSet<string>();

    public int PressedCount => pressedButtonIds.Count;
    public bool IsComplete => pressedButtonIds.Count >= totalButtonsRequired;

    public void RegisterPress(string buttonId)
    {
        if (string.IsNullOrEmpty(buttonId)) return;

        bool added = pressedButtonIds.Add(buttonId);
        if (logProgress && added)
            Debug.Log($"Pressed: {buttonId} ({PressedCount}/{totalButtonsRequired})");
    }

    // Optional if you ever want to clear progress without reloading
    public void ResetProgress()
    {
        pressedButtonIds.Clear();
    }
}
