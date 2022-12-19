using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossDeathPortal : MonoBehaviour
{
    // The prefab of the portal to spawn
    public GameObject portalPrefab;
    // The position at which to spawn the portal
    public Vector2 spawnPosition;
    // A reference to the boss's health script
    public Health health;
    // A flag to track whether the portal has been spawned
    private bool portalSpawned;

    void Update()
    {
        // If the boss is dead and the portal has not yet been spawned, spawn the portal
        if (health.currentHealth <= 0 && !portalSpawned)
        {
            // Spawn the portal
            Instantiate(portalPrefab, spawnPosition, Quaternion.identity);
            // Set the portalSpawned flag to true to prevent the portal from being spawned again
            portalSpawned = true;
        }
    }
}