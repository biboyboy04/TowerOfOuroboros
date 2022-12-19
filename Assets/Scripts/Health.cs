using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class Health : MonoBehaviour
{
    [Header ("Health")]
    [SerializeField] public float startingHealth;
    public float currentHealth { get; private set; }
    private Animator anim;
    public bool dead;
    public static bool playerDead;

    [Header("iFrames")]
    [SerializeField] private float iFramesDuration;
    [SerializeField] private int numberOfFlashes;
    private SpriteRenderer spriteRend;

    [Header("Components")]
    [SerializeField] private Behaviour[] components;
    public bool invulnerable;

    private Transform currentCheckpoint;
    public GameManagerScript gameManager;
    public BorderHealthBar borderHealthBar;

    public GameObject deathCountObject;
    private TMP_Text deathCountText;

    public GameObject portalPrefab;

    public int highestFloorCompleted;
    public string floorName;
    public int currentFloor;

    public GameObject thankYouPanel;


    public GameObject[] itemsToDrop;

    [SerializeField] private AudioSource checkpointSound;

    [SerializeField] private AudioSource playerHurtSound;
    [SerializeField] private AudioSource playerDeadSound;

    [SerializeField] private AudioSource ghostHurtSound;
    [SerializeField] private AudioSource ghostDeadSound;

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
        


        if((PlayerPrefs.GetInt("deathCount") == null)) 
        {
            PlayerPrefs.SetInt("deathCount", 0);
        }
        else
        {
            PlayerPrefs.SetInt("deathCount", PlayerPrefs.GetInt("deathCount"));
        }
        
        if(deathCountObject!=null)
        {
            deathCountText.text = "DEATH COUNT: " + PlayerPrefs.GetInt("deathCount");
        }
        


        if((PlayerPrefs.GetInt("levelReached") == null)) 
        {
            PlayerPrefs.SetInt("levelReached", 1);
        }
    }

    private void Update() 
    {
        // Kill the player if they fall too deep
        if (transform.position.y < -10)
        {
            TakeDamage(999);
        }

        highestFloorCompleted = PlayerPrefs.GetInt("levelReached");

        floorName = SceneManager.GetActiveScene().name;
        currentFloor = System.Int32.Parse(floorName.Substring(floorName.Length - 1));
        // Debug.Log("Scene number -3" + (SceneManager.GetActiveScene().buildIndex-3 ));
        // Debug.Log("levelReached"+PlayerPrefs.GetInt("levelReached"));

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
                anim.SetTrigger("hurt");
            }
            if (gameObject.tag == "Ghost") 
            {
                ghostHurtSound.Play();
            }

            StartCoroutine(Invunerability());
        }
        else
        {
            if (!dead)
            {
                anim.SetTrigger("die");
                

                //Deactivate all attached component classes
                foreach (Behaviour component in components)
                    component.enabled = false;

                dead = true;
                
                if (gameObject.tag == "Player")
                {
                    playerDead = dead;
                    PlayerPrefs.SetInt("deathCount", PlayerPrefs.GetInt("deathCount") +1 );
                    deathCountText.text = "DEATH COUNT: " + PlayerPrefs.GetInt("deathCount");
                    playerDeadSound.Play();
                    gameManager.gameOver();
                }

                if (gameObject.tag == "Ghost")
                {
                    ghostDeadSound.Play();
                    DropItem();
                }

                if (gameObject.tag == "Chest")
                {
                    chestOpenSound.Play();
                    DropItem();
                }

                if (gameObject.tag == "Miniboss")
                {
                    if(SceneManager.GetActiveScene().name == "Miniboss3")
                    {
                        thankYouPanel.SetActive(true);
                    }
                    ghostDeadSound.Play();
                    DropItem();
                    //DropPortal
                    Debug.Log("Portal dropped");
                    Instantiate(portalPrefab, transform.position = new Vector3(transform.position.x, 
                    Random.Range(1, 1.1f), transform.position.z) , Quaternion.identity);


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

        int enemies = 10;
        int traps = 11;

        if (gameObject.tag == "Player")
        {
            Physics2D.IgnoreLayerCollision(enemies, traps, true);
        }
    
        for (int i = 0; i < numberOfFlashes; i++)
        {
            spriteRend.color = new Color(1, 0, 0, 0.5f);
            yield return new WaitForSeconds(iFramesDuration / (numberOfFlashes * 2));
            spriteRend.color = Color.white;
            yield return new WaitForSeconds(iFramesDuration / (numberOfFlashes * 2));
        }

        Physics2D.IgnoreLayerCollision(enemies, traps, false);
        invulnerable = false;
    }

    private void Deactivate()
    {
        gameObject.SetActive(false);
    }

    //Respawn
    public void Respawn()
    {
        dead = false;
        playerDead = false;
        AddHealth(startingHealth);
        anim.ResetTrigger("die");
        anim.Play("idle");
        //invulnerable = false; 

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

        //Move the camera to the checkpoint's room
        //Camera.main.GetComponent<CameraController>().MoveToNewRoom(currentCheckpoint.parent);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Checkpoint")
        {
            currentCheckpoint = collision.transform;
            checkpointSound.Play();
            collision.GetComponent<SpriteRenderer>().color = new Color(0, 1, 1, 1);
            //spriteRend.color = new Color(1, 0, 0, 0.5f);
            //spriteRend.color = Color.red;
            collision.GetComponent<Collider2D>().enabled = false;
            // collision.GetComponent<Animator>().SetTrigger("activate");
        }
    }

    public void DropItem()
    {

        floorName = SceneManager.GetActiveScene().name;
        if(floorName.Contains("Miniboss"))
        {
            foreach (GameObject itemToDrop in itemsToDrop) 
            {
                // Drop an item at the position of the carrier and put some offset
                Instantiate(itemToDrop, transform.position  += new Vector3(Random.Range(-0.5f, 0.5f), 
                Random.Range(0, 0.1f), transform.position.z) , Quaternion.identity); 
            }
        }
        else 
        {
            currentFloor = System.Int32.Parse(floorName.Substring(floorName.Length - 1));

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
 
}