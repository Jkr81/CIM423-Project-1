using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.XR.Interaction.Toolkit;

public class RestartOnInteract : MonoBehaviour
{
    private UnityEngine.XR.Interaction.Toolkit.Interactables.XRBaseInteractable interactable;

    private void Awake()
    {
        interactable = GetComponent<UnityEngine.XR.Interaction.Toolkit.Interactables.XRBaseInteractable>();
    }

    private void OnEnable()
    {
        if (interactable != null)
            interactable.selectEntered.AddListener(OnSelect);
    }

    private void OnDisable()
    {
        if (interactable != null)
            interactable.selectEntered.RemoveListener(OnSelect);
    }

    private void OnSelect(SelectEnterEventArgs args)
    {
        Debug.Log("Panel clicked → Restarting scene");

        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}