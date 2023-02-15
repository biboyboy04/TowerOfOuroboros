using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemies : MonoBehaviour
{
    [SerializeField] private GameObject enemyToSummon;
    [SerializeField] private float spawnInterval;
    [SerializeField] private int numEnemiesToSpawn;
    [SerializeField] private float spawnRadius;

    public Health playerHealth;
    public AudioSource spawnSound;

    private Health enemyHealth;
    private SpriteRenderer spriteRenderer;
    private float timer;
    bool canPowerUp = true;

    // Start is called before the first frame update
    void Start()
    {
        enemyHealth = this.GetComponent<Health>();
        spriteRenderer = this.GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
  
    }

    void Update() 
    {
       if(playerHealth.currentHealth > 0)
        {
            // Increment the timer by the amount of time that has passed since the last frame
            timer += Time.deltaTime;
        }
        MakeBossStrongWhenHealthIsLow();
        AbilityActivateIndicator();
        EnemySpawn();
    }


    private void AbilityActivateIndicator()
    {
        // If the timer is 1 second before the spawnInterval , change the color of the boss
        if(timer >= spawnInterval -1)
        {
            spriteRenderer.color = new Color(1, 0, 1, 1);
        }
        else
        {
            spriteRenderer.color = Color.white;
        } 
    }


    private void EnemySpawn()
    {
        if (timer >= spawnInterval)
        {
            spawnSound.Play();
            for (int i = 0; i < numEnemiesToSpawn; i++)
            {
                // Calculate a random position within the spawn radius
                Vector3 spawnPos = transform.position + Random.insideUnitSphere * spawnRadius;
                // Instantiate the enemy prefab at the spawn position
                Instantiate(enemyToSummon, spawnPos, Quaternion.identity);
            }
            timer = 0;
        }
    }
    

    private void MakeBossStrongWhenHealthIsLow()
    {
        if(enemyHealth.currentHealth <= (enemyHealth.startingHealth / 2) && canPowerUp)
        {
            spawnInterval /= 2;
            numEnemiesToSpawn = 5;
            canPowerUp = false;
        } 
    }

}
