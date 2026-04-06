using UnityEngine;

public class BrewButton : MonoBehaviour
{
    public CoffeeMachineSimple machine;

    public void PressButton()
    {
        Debug.Log("Brew button pressed");

        if (machine != null)
            machine.PressBrewButton();
        else
            Debug.LogWarning("Machine reference missing");
    }
}