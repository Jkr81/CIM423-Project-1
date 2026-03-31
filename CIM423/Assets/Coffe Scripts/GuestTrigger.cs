using UnityEngine;


public class GuestTrigger : MonoBehaviour
{
    public GameObject goodJobText;
    public CoffeeMachineSimple machine;
    public Transform handPoint;

    private bool alreadyDelivered = false;

    private void OnTriggerEnter(Collider other)
    {
        if (alreadyDelivered)
            return;

        if (!other.CompareTag("Cup"))
            return;

        if (machine == null || !machine.coffeeReady)
        {
            Debug.Log("This cup is empty!");
            return;
        }

        GameObject cupRoot = other.transform.root.gameObject;

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

        // Disable XR grab
        UnityEngine.XR.Interaction.Toolkit.Interactables.XRGrabInteractable grab = cup.GetComponent<UnityEngine.XR.Interaction.Toolkit.Interactables.XRGrabInteractable>();
        if (grab != null)
        {
            grab.enabled = false;
        }

        // Stop physics
        Rigidbody rb = cup.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.linearVelocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
            rb.isKinematic = true;
            rb.useGravity = false;
        }

        // Parent first, then snap
        cup.transform.SetParent(handPoint);
        cup.transform.localPosition = Vector3.zero;
        cup.transform.localRotation = Quaternion.identity;

        Debug.Log("Cup attached to guest hand point");
    }
}