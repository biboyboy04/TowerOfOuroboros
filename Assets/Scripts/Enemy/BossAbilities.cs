using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAbilities : MonoBehaviour
{
    // The animation to play
    private Animator anim;
    // The interval at which to play the animation, in seconds
    public float interval;
    // A timer to track when to play the animation
    private float timer;

    #region SPAWN
    public GameObject enemyPrefab;
    // The number of enemies to summon
    public int enemyCount;
    // The radius around the summoner to spawn the enemies
    public float spawnRadius;

    public float teleportDistance = 100f; // The distance at which the enemy will teleport
    public GameObject player; // The player game object

    public Health playerHealth;
    public bool IsFacingRight { get; private set; }

    #endregion

    public bool isSpawn;
    public bool isTeleport;

    public bool canActivateAbility;


    void Start()
    {
        anim = GetComponent<Animator>();
        IsFacingRight = true;
    }

    void Update()
    {
        if(playerHealth.currentHealth > 0)
        {
            AbilityActivateIndicator();
            // Increment the timer by the amount of time that has passed since the last frame
            timer += Time.deltaTime;
            // If the timer has reached the interval, it's time to play the animation

            if (timer >= interval)
            {   
                canActivateAbility = true;
                if(isSpawn)
                {
                    SpawnEnemies();
                    timer = 0;
                }

                if(isTeleport)
                {
                    TeleportBehind();
                    timer = 0;
                }
                
                // Reset the timer
                
                // Play the animation

            }
            else
            {
                canActivateAbility = false;
            }
        }
    }

    void AbilityActivateIndicator()
    {
        if(timer >= interval-1)
        {
            this.GetComponent<SpriteRenderer>().color = new Color(1, 0, 0, 1);
        }
        else
        {
            this.GetComponent<SpriteRenderer>().color = Color.white;
        } 
    }

    void SpawnEnemies()
    {
        for (int i = 0; i < enemyCount; i++)
        {
            // Calculate a random position within the spawn radius
            Vector3 spawnPos = transform.position + Random.insideUnitSphere * spawnRadius;
            // Instantiate the enemy prefab at the spawn position
            Instantiate(enemyPrefab, spawnPos, Quaternion.identity);
        }
    }

    void TeleportBehind()
    {
        // Calculate the distance between the enemy and the player
        float distance = Vector2.Distance(transform.position, player.transform.position);

        // If the distance is less than the teleport distance, teleport the enemy
        if (distance < teleportDistance)
        {
            if(this.transform.position.x > player.transform.position.x)
            {
                //Get position to teleport at the back left side of the player
                Vector2 behindPlayer = player.transform.position - player.transform.right * 5f;
                transform.position = behindPlayer;
            } 

            // If enemy is at the left side of the player
            else
            { 
                //Get position to teleport at the back right side of the player
                Vector2 behindPlayer = player.transform.position - player.transform.right * -5f;
                transform.position = behindPlayer;
            }
            // Calculate the position behind the player

            // Set the enemy's position to the calculated position behind the player
            
        }
    }


}