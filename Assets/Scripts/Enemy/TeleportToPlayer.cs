using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportToPlayer : MonoBehaviour
{
    // The animation to play
    private Animator anim;
    // The interval at which to play the animation, in seconds
    public float interval;
    // A timer to track when to play the animation
    private float timer;

    public float teleportDistance = 100f; // The distance at which the enemy will teleport
    public GameObject player; // The player 
    public Health playerHealth;
    private Health health;

    public bool canActivateAbility;
    public bool activatingAbility;
    public ChargeAttack chargeAttack;

    private float offsetPosition;

    public AudioSource bossTeleportSound;

    bool canChangeValue = true;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();     
    }

    void FixedUpdate() 
    {
        AbilityActivateIndicator();
    }
    
    // Update is called once per frame
    void Update()
    {   

        if(IsHalfHealth())
        {
            canChangeValue = false;
            interval/=2;
        }
        
        if(playerHealth.currentHealth > 0)
        {
            if(chargeAttack!=null && chargeAttack.canTeleport)
            {
                chargeAttack.canTeleport= false;
                canActivateAbility = true;
                offsetPosition = 2;
                TeleportBehind();
            }
            else
            {
                offsetPosition = 1f;
            }
            // Increment the timer by the amount of time that has passed since the last frame
            timer += Time.deltaTime;

            // If the timer has reached the interval, it's time to do something
            if (timer >= interval)
            {   
                canActivateAbility = true;
                TeleportBehind();
                timer = 0;
            }
            
            else
            {
                canActivateAbility = false;
            }
        }
    }

    void AbilityActivateIndicator()
    {
        // If the timer is 1 second before the interval, change the color of the boss
        if(timer >= interval-1)
        {   
            activatingAbility = true;
            anim.SetTrigger("teleport");
            this.GetComponent<SpriteRenderer>().color = new Color(0, 1, 0, 1);
        }
        else
        {
            activatingAbility = false;
            this.GetComponent<SpriteRenderer>().color = Color.white;
        } 
    }

     void TeleportBehind()
    {
        bossTeleportSound.Play();
        // Calculate the distance between the enemy and the player
        float distance = Vector2.Distance(transform.position, player.transform.position);

        // If the distance is less than the teleport distance, teleport the enemy
        if (distance < teleportDistance)
        {
            if(this.transform.position.x > player.transform.position.x)
            {
                //Get position to teleport at the back left side of the player
                Vector2 behindPlayer = player.transform.position - player.transform.right * offsetPosition;

                // Set the enemy's position to the calculated position behind the player
                transform.position = behindPlayer;
            } 

            // If enemy is at the left side of the player
            else
            { 
                //Get position to teleport at the back right side of the player
                Vector2 behindPlayer = player.transform.position - player.transform.right * -offsetPosition;

                // Set the enemy's position to the calculated position behind the player
                transform.position = behindPlayer;
            }     
            
        }
    }

    private bool IsHalfHealth()
    {   

        health = this.GetComponent<Health>();
        if(canChangeValue)
        {
            return health.currentHealth <= (health.startingHealth / 2);
        }
        else
            return false;
    }
}
