using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class PanelController : MonoBehaviour
{
    private UnityEngine.XR.Interaction.Toolkit.Interactables.XRSimpleInteractable interactable;
    
    void Start()
    {
        interactable = GetComponent<UnityEngine.XR.Interaction.Toolkit.Interactables.XRSimpleInteractable>();
        interactable.selectEntered.AddListener(OnPanelClicked);
    }
    
    void OnPanelClicked(SelectEnterEventArgs args)
    {
        Debug.Log("Panel clicked!");
        gameObject.SetActive(false);
    }
}