using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportToPlayer : MonoBehaviour
{
    private Animator anim;
    private Health enemyHealth;
    private ChargeAttack chargeAttack;
    
    public float teleportCooldown;
    public float teleportDistance = 100f;
    public Health playerHealth;
    public Transform playerTransform;
    public AudioSource bossTeleportSound;

    private float timer;
    private float distanceToPlayer;
    private float teleportOffset = 1f;
    private bool valueChanged = false;
    private Vector2 playerFront;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>(); 
        enemyHealth = this.GetComponent<Health>();
        chargeAttack = this.GetComponent<ChargeAttack>();
    }

    // Update is called once per frame
    void Update()
    {   
        if(playerHealth.currentHealth > 0)
        {
            timer += Time.deltaTime;

            // Change into the teleport animation 1 second before the cooldown 
            // as an indicator that the enemy will teleport
            anim.SetBool("teleport", (timer >= teleportCooldown-1));
            
            MakeBossStrongWhenHealthIsLow();
            Teleport(timer >= teleportCooldown);

            if(chargeAttack != null)
            {
                Teleport(chargeAttack.canTeleport);
                chargeAttack.canTeleport = false;
            }
        }
    }


    private void Teleport(bool canTeleport)
    {
        if(canTeleport)
        {
            bossTeleportSound.Play();
            distanceToPlayer = Vector2.Distance(transform.position, playerTransform.transform.position);

            if (distanceToPlayer < teleportDistance)
            {
                // If enemy is at the right side of the player
                if(this.transform.position.x > playerTransform.transform.position.x)
                {
                    playerFront = playerTransform.transform.position - playerTransform.transform.right * teleportOffset;
                } 
                else
                { 
                    playerFront = playerTransform.transform.position - playerTransform.transform.right * -teleportOffset;
                }   

                // Set the enemy's position to the calculated position at the front of the player 
                transform.position = playerFront; 
            }
            // Reset the timer
            timer = 0;
        }
    }


    private void MakeBossStrongWhenHealthIsLow()
    {
        if(enemyHealth.currentHealth <= (enemyHealth.startingHealth / 2) && !valueChanged)
        {
            teleportCooldown/=2;
            valueChanged = true;
        } 
    }
}
