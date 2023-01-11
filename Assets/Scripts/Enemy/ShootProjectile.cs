using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootProjectile : MonoBehaviour
{
    public float speed;
    public float lineOfSite;
    public float shootingRange;

    public GameObject bullet;
    public GameObject bulletParent;
    public Transform player;
    public GameObject playerObject;
    
    public bool isBoss;
    public float attackCooldown = 8f;
    private float cooldownTimer;
    
    private float changeAttackCooldownTimer;
    public float changeAttackCooldown;
    
    public AudioSource projectileSounds;

    private Animator anim;

    public bool IsFacingRight { get; private set; }

    private Health enemyHealth;
    public Health playerHealth;

    public AudioSource rapidFireSound;

    bool canChangeValue = true;
    

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        IsFacingRight = true;
        enemyHealth = this.GetComponent<Health>();
    }

    void FixedUpdate() 
    {
        if(isBoss)
        {
            AbilityActivateIndicator();
            AttackIndicator();
        }
        
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
         
        if(playerHealth.currentHealth > 0)
        {
            cooldownTimer += Time.deltaTime;
            float distanceFromPlayer = Vector2.Distance(player.position, transform.position);

            
            if(isBoss)
            {

                if(IsHalfHealth())
                {
                    canChangeValue = false;
                    anim.SetBool("rage", true);
                }

                if(enemyHealth.invulnerable == false && playerHealth.invulnerable == false)
                {
                    transform.position = Vector2.MoveTowards(this.transform.position, player.position, speed * Time.deltaTime);
                }
                if(cooldownTimer >= attackCooldown)
                {
                    cooldownTimer = 0;
                    projectileSounds.Play();
                    Instantiate(bullet, bulletParent.transform.position, Quaternion.identity);
                }

                changeAttackCooldownTimer += Time.deltaTime;
                if(changeAttackCooldownTimer>=changeAttackCooldown)
                {
                    changeAttackCooldownTimer=0;
                    anim.SetTrigger("changeAttackCooldown");
                }
            }
            else
            {
                if(distanceFromPlayer<lineOfSite && distanceFromPlayer > shootingRange)
                {
                    transform.position = Vector2.MoveTowards(this.transform.position, player.position, speed * Time.deltaTime);
                }
                else if(distanceFromPlayer <= shootingRange && cooldownTimer >= attackCooldown)
                {
                    cooldownTimer = 0;
                    projectileSounds.Play();
                    Instantiate(bullet, bulletParent.transform.position, Quaternion.identity);
                }
            }
        }

    }

    private void OnDrawGizmosSelected() 
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, lineOfSite);
        Gizmos.DrawWireSphere(transform.position, shootingRange);
    }

    

    void AttackIndicator()
    {
        if(cooldownTimer >= attackCooldown-1)
        {
            this.GetComponent<SpriteRenderer>().color = new Color(0, 1, 0, 1);
        }
        else
        {
            this.GetComponent<SpriteRenderer>().color = Color.white;
        } 
    }

    void AbilityActivateIndicator()
    {
        if(changeAttackCooldownTimer >= changeAttackCooldown-1)
        {
            rapidFireSound.Play();
            this.GetComponent<SpriteRenderer>().color = new Color(1, 0, 0, 1);
        }
        else
        {
            this.GetComponent<SpriteRenderer>().color = Color.white;
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

    private bool IsHalfHealth()
    {   

        enemyHealth = this.GetComponent<Health>();
        if(canChangeValue)
        {
            return enemyHealth.currentHealth <= (enemyHealth.startingHealth / 2);
        }
        else
            return false;
    }
}
