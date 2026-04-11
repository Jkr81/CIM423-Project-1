using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class GuestTrigger : MonoBehaviour
{
    public GameObject successPanel;
    public CoffeeMachineSimple machine;
    public Transform handPoint;

    private bool alreadyDelivered = false;

    private void Start()
    {
        alreadyDelivered = false;
        Debug.Log("GuestTrigger started, alreadyDelivered reset to false");
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Guest trigger hit by: " + other.name);

        if (alreadyDelivered)
        {
            Debug.Log("Already delivered");
            return;
        }

        if (!other.CompareTag("Cup"))
        {
            Debug.Log("Not a cup");
            return;
        }

        if (machine == null)
        {
            Debug.LogWarning("Machine reference missing");
            return;
        }

        if (!machine.coffeeReady)
        {
            Debug.Log("This cup is empty!");
            return;
        }

        Debug.Log("Correct coffee delivered!");

        GameObject cupRoot = other.attachedRigidbody != null
            ? other.attachedRigidbody.gameObject
            : other.gameObject;

        if (successPanel != null)
        {
            successPanel.SetActive(true);
            Debug.Log("Success panel turned ON");
        }
        else
        {
            Debug.LogWarning("Success Panel is not assigned!");
        }

        SnapCupToHand(cupRoot);

        alreadyDelivered = true;
        Debug.Log("alreadyDelivered set to true");
    }

    void SnapCupToHand(GameObject cup)
    {
        if (handPoint == null)
        {
            Debug.LogWarning("HandPoint is not assigned!");
            return;
        }

        XRGrabInteractable grab = cup.GetComponent<XRGrabInteractable>();
        if (grab != null)
            grab.enabled = false;

        Rigidbody rb = cup.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.linearVelocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
            rb.isKinematic = true;
            rb.useGravity = false;
        }

        cup.transform.SetParent(handPoint);
        cup.transform.localPosition = Vector3.zero;
        cup.transform.localRotation = Quaternion.identity;

        Debug.Log("Cup attached to guest hand point");
    }
}