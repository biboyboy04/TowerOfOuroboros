using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SideAttack : EnemyDamage
{
    [SerializeField] private float speed;
    [SerializeField] private float range;
    [SerializeField] private float checkDelay;
    [SerializeField] private LayerMask playerLayer;
    private Vector3[] directions = new Vector3[4];
    private Vector3 destination;
    private float checkTimer;
    private bool attacking;
    private bool canChangeValue;
    private Health health;
    private bool canChangeColor;
    

    [Header("SFX")]
    [SerializeField] private AudioClip impactSound;
    private void Start()
    {
        canChangeValue = true;
        canChangeColor = false;
    }

    private void OnEnable()
    {
        Stop();
    }
    private void Update()
    {

        if(this.tag == "Miniboss")
        {
            //changeColor();

            if(IsHalfHealth())
            {
                canChangeValue = false;
                speed*=2;
                checkDelay/=2;
            }
        }



        //Move spikehead to destination only if attacking
        if (attacking)
        {
            transform.Translate(destination * Time.deltaTime * speed);
        }

        else
        {
            checkTimer += Time.deltaTime;
            
            if (checkTimer > checkDelay)
            {
                CheckForPlayer();
            }
        }
    }
    private void CheckForPlayer()
    {
        CalculateDirections();
        canChangeColor = true;
        changeColor();
       
        //Check if spikehead sees player in all 4 directions
        for (int i = 0; i < directions.Length; i++)
        {
            Debug.DrawRay(transform.position, directions[i], Color.red);

            RaycastHit2D hit = Physics2D.Raycast(transform.position, directions[i], range, playerLayer);

            if (hit.collider != null && !attacking)
            {
                attacking = true;
                destination = directions[i];
                checkTimer = 0;
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
        attacking = false;
        canChangeColor = false;
        changeColor();
    }

    private void changeColor()
    {
        if(canChangeColor)
        {
            this.GetComponent<SpriteRenderer>().color = new Color(1, 0, 0, 1);
        }
        else
            this.GetComponent<SpriteRenderer>().color = Color.white;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
       // SoundManager.instance.PlaySound(impactSound);
        base.OnTriggerEnter2D(collision);
        Stop(); //Stop spikehead once he hits something
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