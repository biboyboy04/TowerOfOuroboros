using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HorizontalFollow : MonoBehaviour
{
    public float distanceThreshold = 5.0f; // Distance at which the ball will start rolling towards the playerTransform
    public float moveSpeed = 5.0f;

    private Rigidbody2D rb; // Rigidbody2D component attached to the ball
    private Health enemyHealth;
    private Animator anim;

    public Transform playerTransform; // Transform component of the playerTransform
    public Health playerHealth;

    public bool IsFacingRight { get; private set; }

    void Start()
    {
        // Get the Rigidbody2D and Transform components
        rb = GetComponent<Rigidbody2D>();
        IsFacingRight = false;
        enemyHealth = this.GetComponent<Health>();
        anim = this.GetComponent<Animator>();

    }

    void Update()
    {
        // If enemy is at the right side of the playerTransform, flip towards the player
        if(this.transform.position.x > playerTransform.transform.position.x && IsFacingRight)
        {
               Turn();
        } 

        // If enemy is at the left side of the playerTransform
        if(this.transform.position.x < playerTransform.transform.position.x && !IsFacingRight)
        { 
               Turn();
        }

        float distance = Mathf.Abs(transform.position.x - playerTransform.position.x); distance = Vector2.Distance(transform.position, playerTransform.position);

        if (playerHealth.currentHealth > 0)
        {
            // If the distance is less than the threshold, start going towards the player
            if (distance < distanceThreshold && !enemyHealth.invulnerable && !playerHealth.invulnerable)
            {
                MoveHorizontallyTowardsPlayer();
            }
        }
        // Otherwise, stop
        else
        {
            rb.velocity = Vector2.zero;
            
            if(anim!=null)
                anim.SetBool("idle", true);
        }
    }


    private void MoveHorizontallyTowardsPlayer()
    {
        float xMovement = moveSpeed * Mathf.Sign(playerTransform.position.x - transform.position.x);
        rb.velocity = new Vector2(xMovement, rb.velocity.y);

        if (anim != null)
            anim.SetBool("idle", false);
    }


    private void Turn()
	{

		//stores scale and flips the playerTransform along the x axis, 
		Vector3 scale = transform.localScale; 
		scale.x *= -1;
		transform.localScale = scale;
		IsFacingRight = !IsFacingRight;
	}
}