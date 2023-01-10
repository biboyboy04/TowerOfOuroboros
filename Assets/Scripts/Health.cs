using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class Health : MonoBehaviour
{
    [Header ("Greg References")]
    public GameManagerScript gameManager;
    [SerializeField] private GameObject deathCountObject;

    private Transform currentCheckpoint;
    private TMP_Text deathCountText;

    private Animator anim;

    [Header ("Health")]
    [SerializeField] public float startingHealth;
    public float currentHealth { get; private set; }
    public bool dead { get; private set; }
    public bool invulnerable { get; private set; }

    [Header("iFrames")]
    [SerializeField] private float iFramesDuration;
    [SerializeField] private int numberOfFlashes;
    private SpriteRenderer spriteRend;

    [Header("Components")]
    [SerializeField] private Behaviour[] components;

    
    [Header("Drops")]
    [SerializeField] private GameObject bossPortalDrop;
    [SerializeField] private GameObject[] itemsToDrop;
    

    [Header("Audio")]
    [SerializeField] private AudioSource checkpointSound;

    [SerializeField] private AudioSource playerHurtSound;
    [SerializeField] private AudioSource playerDeadSound;   

    [SerializeField] private AudioSource enemyHurtSound;
    [SerializeField] private AudioSource enemyDeadSound;

    [SerializeField] private AudioSource chestOpenSound;

    private void Awake()
    {
        currentHealth = startingHealth;
        anim = GetComponent<Animator>();
        spriteRend = GetComponent<SpriteRenderer>();
        if(deathCountObject!=null)
        {
            deathCountText = deathCountObject.GetComponent<TextMeshProUGUI>();
        }
        
        // If the player's death count is not stored in PlayerPrefs, set it to 0
        if(!PlayerPrefs.HasKey("deathCount"))
        {
            PlayerPrefs.SetInt("deathCount", 0);
        }
        else
        {
            PlayerPrefs.SetInt("deathCount", PlayerPrefs.GetInt("deathCount"));
        }
        
        // Update the death count UI element with the value stored in PlayerPrefs
        if(deathCountObject!=null)
        {
            deathCountText.text = "DEATH COUNT: " + PlayerPrefs.GetInt("deathCount");
        }
        
        // If the highest floor completed is not stored in PlayerPrefs, set it to 1
        if(!PlayerPrefs.HasKey("levelReached"))
        {
            PlayerPrefs.SetInt("levelReached", 1);
        }
    }

    private void Update() 
    {
        // Kill the player if they fall too deep
        if (transform.position.y < -10)
        {
            TakeDamage(9999);
        }
    }

    public void TakeDamage(float _damage)
    {
        if (invulnerable && gameObject.tag == "Player") return;

        currentHealth = Mathf.Clamp(currentHealth - _damage, 0, startingHealth);

        if (currentHealth > 0)
        {
            if (gameObject.tag == "Player")
            {
                playerHurtSound.Play();
            }
            else
            {
                if(enemyHurtSound!= null)
                {
                    enemyHurtSound.Play();
                }
            }

            anim.SetTrigger("hurt");

            StartCoroutine(Invunerability());
        }
        else
        {
            if (!dead)
            {
                // Trigger the die animation, deactivate all attached component classes, and set the dead flag to true
                anim.SetTrigger("die");
                foreach (Behaviour component in components)
                    component.enabled = false;
                dead = true;

                // If the entity is the player, increment the death count, play the player dead sound, and call the game over function
                if (gameObject.tag == "Player")
                {
                    PlayerPrefs.SetInt("deathCount", PlayerPrefs.GetInt("deathCount") + 1);
                    deathCountText.text = "DEATH COUNT: " + PlayerPrefs.GetInt("deathCount");
                    gameManager.gameOver();
                }
                else if (gameObject.tag == "Chest")
                {
                    chestOpenSound.Play();
                    DropItem();
                }
                else
                {
                    if(enemyDeadSound!= null)
                    {
                        enemyDeadSound.Play();
                    }
                    DropItem();
                }
                
                if (gameObject.tag == "Boss")
                {
                    DropItem();
 
                    Instantiate(bossPortalDrop, transform.position, Quaternion.identity);
                }
                
            }
        }
    }

    public void AddHealth(float _value)
    {
        currentHealth = Mathf.Clamp(currentHealth + _value, 0, startingHealth);
    }

    private IEnumerator Invunerability()
    {
        invulnerable = true;


        int enemyLayerNumber = 10;
        int trapLayerNumber = 11;

        if (gameObject.tag == "Player")
        {
            Physics2D.IgnoreLayerCollision(enemyLayerNumber, trapLayerNumber, true);
        }
    
        for (int i = 0; i < numberOfFlashes; i++)
        {
            spriteRend.color = new Color(1, 0, 0, 0.5f);
            yield return new WaitForSeconds(iFramesDuration / (numberOfFlashes * 2));
            spriteRend.color = Color.white;
            yield return new WaitForSeconds(iFramesDuration / (numberOfFlashes * 2));
        }

        Physics2D.IgnoreLayerCollision(enemyLayerNumber, trapLayerNumber, false);
        invulnerable = false;
    }

    //Respawn
    public void Respawn()
    {
        dead = false;
        AddHealth(startingHealth);
        anim.ResetTrigger("die");
        anim.Play("idle");

        //Activate all attached component classes
        foreach (Behaviour component in components)
            component.enabled = true;
    }

    public void RespawnToCheckpoint()
    {
        Respawn();//Restore player health and reset animation
        
        if (currentCheckpoint == null)
        {
            gameManager.restart();
            invulnerable = false;
        }
        transform.position = currentCheckpoint.position; //Move player to checkpoint location 
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Checkpoint")
        {
            currentCheckpoint = collision.transform;
            checkpointSound.Play();

            //Change checkpoint color to blue
            collision.GetComponent<SpriteRenderer>().color = new Color(0, 1, 1, 1);

            collision.GetComponent<Collider2D>().enabled = false;
        }
    }

    public void DropItem()
    {
        Vector3 originalTransformPos = transform.position;

        if(itemsToDrop != null && (SceneManager.GetActiveScene().buildIndex-3) >= PlayerPrefs.GetInt("levelReached"))
        {

            foreach (GameObject itemToDrop in itemsToDrop) 
            {
                // Drop an item at the position of the carrier and put some offset
                Instantiate(itemToDrop, transform.position  += new Vector3(Random.Range(-0.5f, 0.5f), 
                Random.Range(0, 0.1f), transform.position.z) , Quaternion.identity); 
            }
        }
        transform.position = originalTransformPos;
        
    }
 
}