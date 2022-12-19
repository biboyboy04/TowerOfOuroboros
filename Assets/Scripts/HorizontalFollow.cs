using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HorizontalFollow : MonoBehaviour
{
    public float moveSpeed = 5.0f; // the speed at which the boss will move towards the player

    private Transform playerTransform; // a reference to the player's transform component
    private Rigidbody2D bossRigidbody; // a reference to the boss's rigidbody component

    bool IsFacingRight;
    public GameObject player;

    [Header("Attack Parameters")]
    [SerializeField] private float attackCooldown;
    [SerializeField] private float range;
    [SerializeField] private int damage;

    [Header("Collider Parameters")]
    [SerializeField] private float colliderDistance;
    [SerializeField] private BoxCollider2D boxCollider;

    [Header("Player Layer")]
    [SerializeField] private LayerMask playerLayer;
    private float cooldownTimer = Mathf.Infinity;

        //References
    private Animator anim;
    private Health playerHealth;

    void Start()
    {
        // get references to the player's transform and the boss's rigidbody
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        bossRigidbody = GetComponent<Rigidbody2D>();
        IsFacingRight = false;
        anim = GetComponent<Animator>();
    }

    void Update()
    {

        if(this.transform.position.x > player.transform.position.x && IsFacingRight){
               Turn();
        } 

        // If enemy is at the left side of the player
        if(this.transform.position.x < player.transform.position.x && !IsFacingRight){ 
               Turn();
        }
        // calculate the distance between the boss and the player
        float distance = Mathf.Abs(transform.position.x - playerTransform.position.x);


         cooldownTimer += Time.deltaTime;
        // if the distance between the boss and the player is less than the attack range, attack the player
        if (PlayerInSight())
        {
            if (cooldownTimer >= attackCooldown)
            {
                cooldownTimer = 0;
                anim.SetTrigger("attack");
            }
        }

        else
        {
            // otherwise, move towards the player horizontally
            float xMovement = moveSpeed * Mathf.Sign(playerTransform.position.x - transform.position.x);
            bossRigidbody.velocity = new Vector2(xMovement, bossRigidbody.velocity.y);
        }
    }
    

    void Attack()
    {
        // attack the player here
    }

    private void Turn()
	{

		//stores scale and flips the player along the x axis, 
		Vector3 scale = transform.localScale; 
		scale.x *= -1;
		transform.localScale = scale;

		IsFacingRight = !IsFacingRight;
	}

    private bool PlayerInSight()
    {
        RaycastHit2D hit =
            Physics2D.BoxCast(boxCollider.bounds.center + transform.right * range * transform.localScale.x * colliderDistance,
            new Vector3(boxCollider.bounds.size.x * range, boxCollider.bounds.size.y, boxCollider.bounds.size.z),
            0, Vector2.left, 0, playerLayer);

        if (hit.collider != null)
            playerHealth = hit.transform.GetComponent<Health>();

        return hit.collider != null;
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(boxCollider.bounds.center + transform.right * range * transform.localScale.x * colliderDistance,
            new Vector3(boxCollider.bounds.size.x * range, boxCollider.bounds.size.y, boxCollider.bounds.size.z));
    }

    private void DamagePlayer()
    {
        if (PlayerInSight())
            playerHealth.TakeDamage(damage);
    }
    
}
