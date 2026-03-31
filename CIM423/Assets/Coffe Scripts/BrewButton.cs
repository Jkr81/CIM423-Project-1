using UnityEngine;

public class BrewButton : MonoBehaviour
{
    public CoffeeMachineSimple machine;

    public void PressButton()
    {
        machine.PressBrewButton();
    }
}