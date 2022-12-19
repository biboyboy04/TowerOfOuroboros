using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed = 10f; // The speed at which the projectile moves

    void Update()
    {
        // Move the projectile forward by the speed per second
        transform.position += transform.up * speed * Time.deltaTime;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // If the projectile collides with something, destroy it
        Destroy(gameObject);
    }
}
