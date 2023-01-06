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

    [SerializeField] public float damage;
    [SerializeField] private AudioSource swordSlashSound;

    // Start is called before the first frame update
    void Start()
    {
        RB = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (CanAttack())
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                Attack();
                swordSlashSound.Play();
                nextAttackTime = Time.time + 1f / attackRate;
            }
        }
    }

    public void Attack()
    {
        if (CanAttack())
        {
            //Play attack animation
            animator.SetTrigger("attack");
            // Detect enemies in range
            Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);

            // Damage Enemies

            foreach (Collider2D enemy in hitEnemies)
            {
                Debug.Log("We hit" + enemy.name);
                enemy.GetComponent<Health>()?.TakeDamage(PlayerPrefs.GetFloat("playerDamage", 10));
                (enemy.GetComponent<EnemyDamage>() as Behaviour).enabled = false;
            }

            swordSlashSound.Play();
            nextAttackTime = Time.time + 1f / attackRate;
         }
    }

    public bool CanAttack()
    {
        return Time.time >= nextAttackTime;
    }

    void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
            return;

        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }

 }


