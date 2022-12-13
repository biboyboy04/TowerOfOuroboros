using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFollow : MonoBehaviour
{
    public GameObject player;
    public float speed;
    private SpriteRenderer spriteRend;
    private Color originColor;
    public float distanceBetween;
    public bool IsFacingRight { get; private set; }
    public Health playerHealth;
    public GameObject[] lights;
    private float distance;
    private Animator animator;
    private Health enemyHealth;


    // Start is called before the first frame update
    void Start()
    {
        spriteRend = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        enemyHealth = this.GetComponent<Health>();
        IsFacingRight = true;

    }

    // Update is called once per frame
    void Update()

    {
        // If enemy is at the right side of the player
        if(this.transform.position.x > player.transform.position.x && IsFacingRight){
               Turn();
        } 

        // If enemy is at the left side of the player
        if(this.transform.position.x < player.transform.position.x && !IsFacingRight){ 
               Turn();
        }
        
        distance = Vector2.Distance(transform.position, player.transform.position);
        Vector2 direction = player.transform.position - transform.position;
        direction.Normalize();

        Vector2 playerPosition = new Vector2(player.transform.position.x, player.transform.position.y - 0.1f);

        if (distance < distanceBetween && enemyHealth.invulnerable == false && playerHealth.invulnerable == false)
        {
            transform.position = Vector2.MoveTowards(this.transform.position, playerPosition, speed * Time.deltaTime/2);
            
            // Enable all lights in the array
            foreach (GameObject light in lights) { light.SetActive(true); }
            //animator.enabled = false;
        }

        else
        {     
            //disabling the Light
            foreach (GameObject light in lights) { light.SetActive(false); }
             //animator.enabled = true;
        }

    }

    private void Turn()
	{
		//stores scale and flips the player along the x axis, 
		Vector3 scale = transform.localScale; 
		scale.x *= -1;
		transform.localScale = scale;

		IsFacingRight = !IsFacingRight;
	}
}
