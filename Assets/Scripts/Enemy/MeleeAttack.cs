using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeAttack : MonoBehaviour
{
    public float moveSpeed = 5.0f; // the speed at which the boss will move towards the player
    private Rigidbody2D rb; // a reference to the boss's rigidbody component

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
    private Health enemyHealth;

    public Health playerHealth;


    private void Awake()
    {
        anim = GetComponent<Animator>();
    }


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        enemyHealth = this.GetComponent<Health>();
    }

    void Update()
    {
        if(playerHealth.currentHealth > 0 || playerHealth.currentHealth == null)
        {

            anim.SetBool("idle", rb.velocity == Vector2.zero);

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
        }
        else
        {
            anim.SetBool("idle", true);
        } 

        
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

    //Use this on animationm add event
    private void DamagePlayer()
    {
        if (PlayerInSight())
            playerHealth.TakeDamage(damage);
    }
    
}
