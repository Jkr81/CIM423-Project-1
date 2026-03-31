using UnityEngine;
using System.Collections;

public class CoffeeMachineSimple : MonoBehaviour
{
    [Header("State")]
    public bool hasWater = false;
    public bool hasBeans = false;
    public bool hasCup = false;
    public bool isBrewing = false;
    public bool coffeeReady = false;

    [Header("References")]
    public GameObject coffeeLiquid; // object inside cup, start OFF

    [Header("Effects (Optional)")]
    public ParticleSystem coffeeStream;
    public AudioSource brewSound;
    public float brewTime = 3f;

    public void AddWater()
    {
        if (hasWater) return;

        hasWater = true;
        Debug.Log("Water added");
    }

    public void AddBeans()
    {
        if (hasBeans) return;

        hasBeans = true;
        Debug.Log("Coffee added");
    }

    public void PlaceCup()
    {
        if (hasCup) return;

        hasCup = true;
        Debug.Log("Cup placed");
    }

    public void RemoveCup()
    {
        hasCup = false;
        Debug.Log("Cup removed");
    }

    public void PressBrewButton()
    {
        if (isBrewing)
        {
            Debug.Log("Already brewing");
            return;
        }

        if (!hasWater)
        {
            Debug.Log("Add water first");
            return;
        }

        if (!hasBeans)
        {
            Debug.Log("Add coffee first");
            return;
        }

        if (!hasCup)
        {
            Debug.Log("Place cup first");
            return;
        }

        StartCoroutine(BrewCoffee());
    }

    IEnumerator BrewCoffee()
    {
        isBrewing = true;
        coffeeReady = false;

        Debug.Log("Coffee brewing...");

        if (coffeeStream != null)
            coffeeStream.Play();

        if (brewSound != null)
            brewSound.Play();

        yield return new WaitForSeconds(brewTime);

        if (coffeeLiquid != null)
            coffeeLiquid.SetActive(true);

        coffeeReady = true;
        Debug.Log("Coffee done!");

        if (coffeeStream != null)
            coffeeStream.Stop();

        isBrewing = false;
    }
}