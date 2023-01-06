using UnityEngine;

public class EnemyDamage : MonoBehaviour
{
    [SerializeField] protected float damage;

    private GameObject playerGameObject;

    private PlayerMovement playerMovement;
    private Health playerHealth;

    void Start() 
    {
        playerGameObject = GameObject.FindWithTag("Player");
    }

    protected void OnTriggerEnter2D(Collider2D collision)
    {   
        if(playerGameObject!=null)
            playerMovement = playerGameObject.GetComponent<PlayerMovement>();

        if (collision.tag == "Player")
        {
            playerHealth = collision.GetComponent<Health>();

            // If the damage doesn't kill the player, apply knockback.
            if (damage < playerHealth.currentHealth)
            {
                // Calculate the direction of the knockback.
                bool knockFromRight = collision.transform.position.x <= transform.position.x;

                // Apply the knockback using the player's PlayerMovement script.
                playerMovement.ApplyKnockback(knockFromRight);
            }

            playerHealth.TakeDamage(damage);
        }
    }
}
