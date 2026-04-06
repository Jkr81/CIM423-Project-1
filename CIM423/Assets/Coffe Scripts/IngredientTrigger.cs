using UnityEngine;

public class IngredientTrigger : MonoBehaviour
{
    public enum IngredientType
    {
        Water,
        Beans
    }

    public IngredientType ingredientType;

    public GameObject targetObject; // 👈 assign your "metal" object here

    private void OnTriggerEnter(Collider other)
    {
        // ✅ ONLY trigger if it's the exact object you want
        if (other.gameObject != targetObject)
            return;

        CoffeeMachineSimple machine = other.GetComponentInParent<CoffeeMachineSimple>();

        if (machine == null)
            return;

        Debug.Log("Hit correct target: " + other.name);

        if (ingredientType == IngredientType.Water)
        {
            machine.AddWater();
            Debug.Log("Water added");
        }
        else if (ingredientType == IngredientType.Beans)
        {
            machine.AddBeans();
            Debug.Log("Beans added");
        }

        // ✅ destroy THIS ingredient object
        Destroy(gameObject);
    }
}