using UnityEngine;

public class CupTrigger : MonoBehaviour
{
    public CoffeeMachineSimple machine;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Cup"))
        {
            machine.PlaceCup();
            Debug.Log("Cup placed in machine");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Cup"))
        {
            machine.hasCup = false;
            Debug.Log("Cup removed");
        }
    }
}