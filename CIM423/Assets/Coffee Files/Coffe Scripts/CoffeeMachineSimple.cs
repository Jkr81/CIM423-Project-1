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
    public bool hasBrewedOnce = false; // NEW

    [Header("References")]
    public GameObject coffeeLiquid; // object inside cup, start OFF

    [Header("Brew Button Visual")]
    public Renderer brewButtonRenderer;
    public Color notReadyColor = Color.gray;
    public Color readyToBrewColor = Color.red;
    public Color brewingColor = Color.yellow;
    public Color doneColor = Color.green;

    [Header("Effects (Optional)")]
    public ParticleSystem coffeeStream;
    public AudioSource brewSound;
    public float brewTime = 3f;

    private void Start()
    {
        if (coffeeLiquid != null)
            coffeeLiquid.SetActive(false);

        UpdateBrewButtonColor();
    }

    public void AddWater()
    {
        if (hasWater || hasBrewedOnce) return;

        hasWater = true;
        Debug.Log("Water added");
        UpdateBrewButtonColor();
    }

    public void AddBeans()
    {
        if (hasBeans || hasBrewedOnce) return;

        hasBeans = true;
        Debug.Log("Coffee added");
        UpdateBrewButtonColor();
    }

    public void PlaceCup()
    {
        if (hasCup) return;

        hasCup = true;
        Debug.Log("Cup placed");
        UpdateBrewButtonColor();
    }

    public void RemoveCup()
    {
        hasCup = false;

        if (!hasBrewedOnce)
            coffeeReady = false;

        Debug.Log("Cup removed");
        UpdateBrewButtonColor();
    }

    public void PressBrewButton()
    {
        if (hasBrewedOnce)
        {
            Debug.Log("This machine already brewed once.");
            return;
        }

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
        UpdateBrewButtonColor();

        Debug.Log("Coffee brewing...");

        if (coffeeStream != null)
            coffeeStream.Play();

        if (brewSound != null)
            brewSound.Play();

        yield return new WaitForSeconds(brewTime);

        if (coffeeLiquid != null)
            coffeeLiquid.SetActive(true);

        isBrewing = false;
        coffeeReady = true;
        hasBrewedOnce = true; // LOCK machine after first brew

        Debug.Log("Coffee done!");

        if (coffeeStream != null)
            coffeeStream.Stop();

        UpdateBrewButtonColor();
    }

    private bool IsReadyToBrew()
    {
        return hasWater && hasBeans && hasCup && !hasBrewedOnce;
    }

    private void UpdateBrewButtonColor()
    {
        if (brewButtonRenderer == null)
            return;

        if (coffeeReady)
        {
            brewButtonRenderer.material.color = doneColor;
        }
        else if (isBrewing)
        {
            brewButtonRenderer.material.color = brewingColor;
        }
        else if (IsReadyToBrew())
        {
            brewButtonRenderer.material.color = readyToBrewColor;
        }
        else
        {
            brewButtonRenderer.material.color = notReadyColor;
        }
    }
}