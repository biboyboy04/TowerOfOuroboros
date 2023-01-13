using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChargeAttack : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float range;
    [SerializeField] private float chargeCooldown;
    [SerializeField] private LayerMask playerLayer;

    private Vector3[] directions = new Vector3[4];
    private Vector3 destination;
    private Health enemyHealth;
    private SpriteRenderer spriteRenderer;
    private Animator anim;

    private float chargeCooldownTimer;
    private bool charging;
    private bool canPowerUp = true;
    private float teleportTimer;

    public bool canTeleport { get; private set; }

    [Header("SFX")]
    public AudioSource abilityActivateSound;
    public AudioSource impactSound;

    private void Start()
    {
        anim = GetComponent<Animator>();
        enemyHealth = this.GetComponent<Health>();
        spriteRenderer = this.GetComponent<SpriteRenderer>();
    }

    private void OnEnable()
    {
        Stop();
    }

    private void Update()
    {
        Debug.Log("teleportTimer"+teleportTimer);
        anim.SetBool("charging", charging);

        if(charging)
        {
            ActivateOuroborosTeleport();

            // Charge to the player's position
            transform.Translate(destination * Time.deltaTime * speed);
        }
        else
        {
            chargeCooldownTimer += Time.deltaTime;

            MakeBossStrongWhenHealthIsLow();
            SetChargeReadyColor(false);
            canTeleport = false;

            // Change color 2 second before the delay as an indicator that the enemy will charge    
            if(chargeCooldownTimer > chargeCooldown - 2)
            {
                SetChargeReadyColor(true);
                abilityActivateSound.Play();
            }
            
            if (chargeCooldownTimer > chargeCooldown)
            {
                CheckForPlayer();
            }
        }
    }


    private void CheckForPlayer()
    {
        CalculateDirections();
        //Check if enemy sees player in all 4 directions
        for (int i = 0; i < directions.Length; i++)
        {
            // Reduce the raycast for player check because MudTitan is too tall
            if(gameObject.name == "MudTitan") 
            {
                Vector2 newPosition = transform.position;
                newPosition.y -= 2;
                RaycastHit2D hit = Physics2D.Raycast(newPosition, directions[i], range, playerLayer);
                if (hit.collider != null && !charging)
                {
                    charging = true;
                    destination = directions[i];
                    chargeCooldownTimer = 0;
                }
            }
            else
            {
                RaycastHit2D hit = Physics2D.Raycast(transform.position, directions[i], range, playerLayer);
                if (hit.collider != null && !charging)
                {
                    charging = true;
                    destination = directions[i];
                    chargeCooldownTimer = 0;
                }
            }

        }
    }


    private void CalculateDirections()
    {
        directions[0] = transform.right * range; //Right direction
        directions[1] = -transform.right * range; //Left direction
        directions[2] = transform.up * range; //Up direction
        directions[3] = -transform.up * range; //Down direction
    }


    private void Stop()
    {
        destination = transform.position; //Set destination as current position so it doesn't move
        charging = false;
        SetChargeReadyColor(false);
    }


    private void SetChargeReadyColor(bool chargeReady)
    {
        if(chargeReady)
        {
            spriteRenderer.material.color = new Color(1, 0, 0, 1);
        }
        else
        {
            spriteRenderer.material.color = Color.white;
        }
            
    }


    protected void OnTriggerEnter2D(Collider2D collision)
    {
        if(charging && impactSound!=null)
        {
            impactSound.Play();
        }
        
        Stop(); //Stop once it hits something
    }


      private void MakeBossStrongWhenHealthIsLow()
    {
        if(enemyHealth.currentHealth <= (enemyHealth.startingHealth / 2) && canPowerUp)
        {
            speed*=1.3f;
            chargeCooldown/=1.7f;
            canPowerUp = false;
        } 
    }


    private void ActivateOuroborosTeleport()
    {
        // Enables Ouroboros teleport after charging when hp is below half
        if(gameObject.name == "Ouroboros" && !canPowerUp)
        {
            teleportTimer+=Time.deltaTime;

            if(teleportTimer >= 2)
            {
                teleportTimer = 0;
                canTeleport = true;
                charging = false;
            }
        }
    }
}