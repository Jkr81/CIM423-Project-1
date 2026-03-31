using UnityEngine;

public class IngredientTrigger : MonoBehaviour
{
    public enum IngredientType
    {
        Water,
        Beans
    }

    public IngredientType ingredientType;
    public CoffeeMachineSimple machine;

    private void OnTriggerEnter(Collider other)
    {
        if (ingredientType == IngredientType.Water && other.CompareTag("Water"))
        {
            Debug.Log("Water poured");
            machine.AddWater();
            Destroy(other.gameObject);
        }

        if (ingredientType == IngredientType.Beans && other.CompareTag("Beans"))
        {
            Debug.Log("Beans poured");
            machine.AddBeans();
            Destroy(other.gameObject);
        }
    }
}