using UnityEngine;

public class RespawnZone : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Respawn zone hit by: " + other.name);

        RespawnToStart respawnable = other.GetComponent<RespawnToStart>();

        if (respawnable == null)
            respawnable = other.GetComponentInParent<RespawnToStart>();

        if (respawnable != null)
        {
            respawnable.RespawnNow();
        }
    }
}