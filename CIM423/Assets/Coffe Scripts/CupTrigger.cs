using UnityEngine;

public class CupTrigger : MonoBehaviour
{
    public CoffeeMachineSimple machine;

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("ENTER by: " + other.name);

        Transform current = other.transform;
        while (current != null)
        {
            Debug.Log("Checking: " + current.name + " tag=" + current.tag);

            if (current.CompareTag("Cup"))
            {
                if (machine != null)
                {
                    machine.PlaceCup();
                    Debug.Log("Cup placed in machine");
                }
                else
                {
                    Debug.LogWarning("Machine reference missing on CupTrigger");
                }
                return;
            }

            current = current.parent;
        }

        Debug.Log("Entered object was not the cup");
    }

    private void OnTriggerExit(Collider other)
    {
        Debug.Log("EXIT by: " + other.name);

        Transform current = other.transform;
        while (current != null)
        {
            if (current.CompareTag("Cup"))
            {
                if (machine != null)
                {
                    machine.RemoveCup();
                    Debug.Log("Cup removed");
                }
                else
                {
                    Debug.LogWarning("Machine reference missing on CupTrigger");
                }
                return;
            }

            current = current.parent;
        }
    }
}