using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class GuestTrigger : MonoBehaviour
{
    public GameObject goodJobText;
    public CoffeeMachineSimple machine;
    public Transform handPoint;

    private bool alreadyDelivered = false;

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Guest trigger hit by: " + other.name);

        if (alreadyDelivered)
            return;

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

        GameObject cupRoot = other.attachedRigidbody != null
            ? other.attachedRigidbody.gameObject
            : other.gameObject;

        Debug.Log("Correct coffee delivered!");

        if (goodJobText != null)
            goodJobText.SetActive(true);

        SnapCupToHand(cupRoot);
        alreadyDelivered = true;
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