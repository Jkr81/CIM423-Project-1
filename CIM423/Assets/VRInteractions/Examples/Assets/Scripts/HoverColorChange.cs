using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class HoverColorChange : MonoBehaviour
{
    public Renderer objectRenderer;
    public Material normalMaterial;
    public Material hoverMaterial;

    private UnityEngine.XR.Interaction.Toolkit.Interactables.XRBaseInteractable interactable;

    void Awake()
    {
        interactable = GetComponent<UnityEngine.XR.Interaction.Toolkit.Interactables.XRBaseInteractable>();
    }

    void OnEnable()
    {
        interactable.hoverEntered.AddListener(OnHoverEnter);
        interactable.hoverExited.AddListener(OnHoverExit);
    }

    void OnDisable()
    {
        interactable.hoverEntered.RemoveListener(OnHoverEnter);
        interactable.hoverExited.RemoveListener(OnHoverExit);
    }

    void OnHoverEnter(HoverEnterEventArgs args)
    {
        objectRenderer.material = hoverMaterial;
    }

    void OnHoverExit(HoverExitEventArgs args)
    {
        objectRenderer.material = normalMaterial;
    }
}
