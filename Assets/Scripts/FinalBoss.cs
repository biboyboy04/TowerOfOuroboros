using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalBoss : MonoBehaviour
{
    public float moveSpeed = 5.0f; // the speed at which the boss will move towards the player

    public Transform playerTransform; // a reference to the player's transform component
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
    public TeleportToPlayer teleport;
    private Health enemyHealth;
    public Health playerHealth;
    bool canChangeValue = true;


    private void Awake()
    {
        anim = GetComponent<Animator>();
    }


    void Start()
    {
        // get references to the player's transform and the boss's rigidbody
       // playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        bossRigidbody = GetComponent<Rigidbody2D>();
        enemyHealth = this.GetComponent<Health>();
        IsFacingRight = false;
    }

    void Update()
    {
        if(playerHealth.currentHealth > 0)
        {
            if(bossRigidbody.velocity == Vector2.zero)
            {
                anim.SetBool("idle", true);
            }
            else
            {
                anim.SetBool("idle", false);
            } 

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

                if(enemyHealth.invulnerable == false && playerHealth.invulnerable == false)
                {
                    // Chek if player is on left or right side
                    if(this.transform.position.x > player.transform.position.x && IsFacingRight){
                        Turn();
                    } 
                    if(this.transform.position.x < player.transform.position.x && !IsFacingRight){ 
                        Turn();
                    }

                    // calculate the distance between the boss and the player
                    float distance = Mathf.Abs(transform.position.x - playerTransform.position.x);
                    //move towards the player horizontally
                    float xMovement = moveSpeed * Mathf.Sign(playerTransform.position.x - transform.position.x);
                    bossRigidbody.velocity = new Vector2(xMovement, bossRigidbody.velocity.y);
                }
            }
        }
        else
        {
            anim.SetBool("idle", true);
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

    private bool IsHalfHealth()
    {   

        if(canChangeValue)
        {
            return enemyHealth.currentHealth <= (enemyHealth.startingHealth / 2);
        }
        else
            return false;
    }
    
}
