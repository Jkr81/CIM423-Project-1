using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class RespawnToStart : MonoBehaviour
{
    private Vector3 startPosition;
    private Quaternion startRotation;
    private Rigidbody rb;
    private XRGrabInteractable grabInteractable;

    private void Start()
    {
        startPosition = transform.position;
        startRotation = transform.rotation;
        rb = GetComponent<Rigidbody>();
        grabInteractable = GetComponent<XRGrabInteractable>();

        Debug.Log(gameObject.name + " saved respawn position: " + startPosition);
    }

    public void RespawnNow()
    {
        Debug.Log(gameObject.name + " is respawning");

        if (grabInteractable != null && grabInteractable.isSelected)
        {
            grabInteractable.interactionManager.SelectExit(
                grabInteractable.firstInteractorSelecting,
                grabInteractable
            );
        }

        if (rb != null)
        {
            rb.linearVelocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
            rb.isKinematic = true;
        }

        transform.position = startPosition;
        transform.rotation = startRotation;

        if (rb != null)
        {
            rb.isKinematic = false;
        }
    }
}