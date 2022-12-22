using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HorizontalFollow : MonoBehaviour
{
    public float distanceThreshold = 5.0f; // Distance at which the ball will start rolling towards the playerTransform
    public float moveSpeed = 5.0f;

    private Rigidbody2D rb; // Rigidbody2D component attached to the ball
    public Transform playerTransform; // Transform component of the playerTransform
    private Health enemyHealth;
    public Health playerHealth;
    private Animator anim;

    public bool IsFacingRight { get; private set; }

    void Start()
    {
        // Get the Rigidbody2D and Transform components
        rb = GetComponent<Rigidbody2D>();
        //playerTransform = GameObject.FindWithTag("playerTransform").transform;
        IsFacingRight = false;
        enemyHealth = this.GetComponent<Health>();
        anim = this.GetComponent<Animator>();

    }

    void Update()
    {
        // If enemy is at the right side of the playerTransform
        if(this.transform.position.x > playerTransform.transform.position.x && IsFacingRight){
               Turn();
        } 

        // If enemy is at the left side of the playerTransform
        if(this.transform.position.x < playerTransform.transform.position.x && !IsFacingRight){ 
               Turn();
        }
        // Calculate the distance between the ball and the playerTransform
        float distance = Mathf.Abs(transform.position.x - playerTransform.position.x); distance = Vector2.Distance(transform.position, playerTransform.position);

        // If the distance is greater than zero, continue rolling towards the playerTransform
        if (distance > 0)
        {
            // If the distance is less than the threshold, start rolling towards the playerTransform
            if (distance < distanceThreshold && enemyHealth.invulnerable == false && playerHealth.invulnerable == false)
            {
                    //move towards the playerTransform horizontally
                    float xMovement = moveSpeed * Mathf.Sign(playerTransform.position.x - transform.position.x);
                    rb.velocity = new Vector2(xMovement, rb.velocity.y);
                    
                    if(anim!=null)
                        anim.SetBool("idle", false);
            }
        }
        // Otherwise, stop the ball
        else
        {
            rb.velocity = Vector2.zero;
            
            if(anim!=null)
                anim.SetBool("idle", true);
        }
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