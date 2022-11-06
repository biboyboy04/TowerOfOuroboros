using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    public Animator animator;
    public Transform attackPoint;
    public float attackRange = 0.5f;
    public LayerMask enemyLayers;

    public float attackRate = 2f;
    float nextAttackTime = 0f;

    [SerializeField] protected float damage;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time >= nextAttackTime)
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                Attack();
                nextAttackTime = Time.time + 1f / attackRate;
            }

            if (Input.GetKeyDown(KeyCode.T))
            {
                //AttackLong();
                animator.SetTrigger("attackLong");
            }
        }
    }


    void Attack()
    {
        //Play attack animation
        animator.SetTrigger("attack");
        // Detect enemies in range
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);

        // Damage Enemies


        foreach (Collider2D enemy in hitEnemies)
        {
            Debug.Log("We hit" + enemy.name);
            enemy.GetComponent<Health>()?.TakeDamage(50);
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

