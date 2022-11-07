using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    public Animator animator;
    public Transform attackPoint;
    public float attackRange = 0.5f;
    public LayerMask enemyLayers;
    public Rigidbody2D RB;

    public float attackRate = 2f;
    float nextAttackTime = 0f;

    [SerializeField] protected float damage;
    [SerializeField] private AudioSource swordSlashSound;

    // Start is called before the first frame update
    void Start()
    {
        RB = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time >= nextAttackTime)
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                Attack();
                swordSlashSound.Play();
                nextAttackTime = Time.time + 1f / attackRate;
            }

            if (Input.GetKeyDown(KeyCode.T))
            {
                //AttackLong();
                animator.SetTrigger("attackLong");
            }
        }
    }

    public void Attack()
    {
        if (Time.time >= nextAttackTime)
        {
            //Play attack animation
            animator.SetTrigger("attack");
            // Detect enemies in range
            Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);

            // Damage Enemies


            foreach (Collider2D enemy in hitEnemies)
            {
                Debug.Log("We hit" + enemy.name);
                //enemy.new Vector2(KBForce, KBForce);
                enemy.GetComponent<Health>()?.TakeDamage(50);
                (enemy.GetComponent<EnemyDamage>() as Behaviour).enabled = false;
                // playerMovement.KBCounter = playerMovement.KBTotalTime;
                
                // if (enemy.transform.position.x <= enemy.position.x)
                // {
                //     enemy.RB.velocity = new Vector2(-5, 5);
                // }
                // if (collision.transform.position.x > transform.position.x)
                // {
                //     enemy.RB.velocity = new Vector2(-5, 5);
                // }
            }

            swordSlashSound.Play();
            nextAttackTime = Time.time + 1f / attackRate;
         }
    }


        // void AttackLong()
        // {
        //     //Play attack animation
        //     animator.SetTrigger("attackLong");
        //   //  Debug.Log(attackPoint);
        //     //attackPoint = AttackPointPierce;
        //     // Detect enemies in range
        //     Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);

        //     // Damage Enemies


        //     foreach (Collider2D enemy in hitEnemies)
        //     {
        //         Debug.Log("We hit" + enemy.name);
        //         enemy.GetComponent<Health>()?.TakeDamage(50);
        //     }

        // }

    void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
            return;

        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }

 }


