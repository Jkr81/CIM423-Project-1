using System.Collections;
using UnityEngine;

public class CoffeeMachineXR : MonoBehaviour
{
    [Header("Cup Settings")]
    public GameObject emptyCupPrefab;
    public GameObject filledCupPrefab;
    public Transform cupSpawnPoint;

    [Header("Effects")]
    public ParticleSystem brewParticles;
    public AudioSource brewSound;

    [Header("Timing")]
    public float brewTime = 3f;

    [Header("Optional Visuals")]
    public GameObject readyLight;
    public GameObject brewingLight;

    private bool isBrewing = false;
    private GameObject currentCup;

    void Start()
    {
        if (readyLight != null)
            readyLight.SetActive(true);

        if (brewingLight != null)
            brewingLight.SetActive(false);
    }

    public void StartCoffeeMaking()
    {
        if (isBrewing)
            return;

        StartCoroutine(BrewCoffee());
    }

    IEnumerator BrewCoffee()
    {
        isBrewing = true;

        if (readyLight != null)
            readyLight.SetActive(false);

        if (brewingLight != null)
            brewingLight.SetActive(true);

        // Remove old cup if one is already there
        if (currentCup != null)
        {
            Destroy(currentCup);
        }

        // Spawn empty cup first
        if (emptyCupPrefab != null && cupSpawnPoint != null)
        {
            currentCup = Instantiate(emptyCupPrefab, cupSpawnPoint.position, cupSpawnPoint.rotation);
        }

        // Start particle effect
        if (brewParticles != null)
        {
            brewParticles.Play();
        }

        // Start sound
        if (brewSound != null)
        {
            brewSound.Play();
        }

        // Wait while brewing
        yield return new WaitForSeconds(brewTime);

        // Stop particles
        if (brewParticles != null)
        {
            brewParticles.Stop();
        }

        // Replace empty cup with filled cup
        if (currentCup != null)
        {
            Destroy(currentCup);
        }

        if (filledCupPrefab != null && cupSpawnPoint != null)
        {
            currentCup = Instantiate(filledCupPrefab, cupSpawnPoint.position, cupSpawnPoint.rotation);
        }

        if (readyLight != null)
            readyLight.SetActive(true);

        if (brewingLight != null)
            brewingLight.SetActive(false);

        isBrewing = false;

        Debug.Log("Coffee is ready!");
    }
}