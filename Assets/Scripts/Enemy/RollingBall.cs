using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RollingBall : MonoBehaviour
{
    public float rollSpeed = 5.0f; // Speed at which the ball will roll towards the player
    public float distanceThreshold = 5.0f; // Distance at which the ball will start rolling towards the player

    private Rigidbody2D rb; // Rigidbody2D component attached to the ball
    private Transform player; // Transform component of the player

    void Start()
    {
        // Get the Rigidbody2D and Transform components
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindWithTag("Player").transform;
    }

    void Update()
    {
        // Calculate the distance between the ball and the player
        float distance = Vector2.Distance(transform.position, player.position);

        // If the distance is greater than zero, continue rolling towards the player
        if (distance > 0)
        {
            // If the distance is less than the threshold, start rolling towards the player
            if (distance < distanceThreshold)
            {
                // Calculate the direction to the player
                Vector2 direction = (player.position - transform.position).normalized;

                // Set the velocity of the ball to roll in the direction of the player at the specified speed
                rb.velocity = direction * rollSpeed;
            }
        }
        // Otherwise, stop the ball
        else
        {
            rb.velocity = Vector2.zero;
        }
    }
}