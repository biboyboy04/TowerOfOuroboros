using UnityEngine;

public class EnemyDamage : MonoBehaviour
{
    [SerializeField] protected float damage;
    PlayerMovement playerMovement;

    protected void OnTriggerEnter2D(Collider2D collision)
    {
        //playerMovement = Greg.GetComponent<PlayerMovement>();
        GameObject greg = GameObject.FindWithTag("Player");
        playerMovement = greg.GetComponent<PlayerMovement>();

        if (collision.tag == "Player")
        {
            playerMovement.KBCounter = playerMovement.KBTotalTime;
            
            if (collision.transform.position.x <= transform.position.x)
            {
                playerMovement.KnockFromRight = true;
            }
            if (collision.transform.position.x > transform.position.x)
            {
                playerMovement.KnockFromRight = false;
            }

            collision.GetComponent<Health>().TakeDamage(damage);
        }
            
    }
}