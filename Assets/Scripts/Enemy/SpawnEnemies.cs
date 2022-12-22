using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemies : MonoBehaviour
{
    // The animation to play
    private Animator anim;
    // The interval at which to play the animation, in seconds
    public float interval;
    // A timer to track when to play the animation
    private float timer;

    public GameObject enemyPrefab;
    // The number of enemies to summon
    public int enemyCount;
    // The radius around the summoner to spawn the enemies
    public float spawnRadius;

    public Health playerHealth;

    public bool canActivateAbility;

    public AudioSource spawnSound;

    bool canChangeValue = true;

    private Health enemyHealth;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame

    void FixedUpdate() 
    {
        AbilityActivateIndicator();
    }
    
    void Update()
    {
        
         if(playerHealth.currentHealth > 0)
        {

            if(IsHalfHealth())
            {
                canChangeValue = false;
                interval/=2;
            }
           // AbilityActivateIndicator();
            // Increment the timer by the amount of time that has passed since the last frame
            timer += Time.deltaTime;
            
            // If the timer has reached the interval, it's time to do something
            if (timer >= interval)
            {   
                canActivateAbility = true;
                Spawn();
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
            this.GetComponent<SpriteRenderer>().color = new Color(1, 0,1, 1);
        }
        else
        {
            this.GetComponent<SpriteRenderer>().color = Color.white;
        } 
    }

    void Spawn()
    {
        spawnSound.Play();
        for (int i = 0; i < enemyCount; i++)
        {
            // Calculate a random position within the spawn radius
            Vector3 spawnPos = transform.position + Random.insideUnitSphere * spawnRadius;
            // Instantiate the enemy prefab at the spawn position
            Instantiate(enemyPrefab, spawnPos, Quaternion.identity);
        }
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
