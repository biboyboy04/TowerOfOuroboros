using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using TMPro;

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
    public static bool invulnerable;

    private Transform currentCheckpoint;
    public GameManagerScript gameManager;
    public BorderHealthBar borderHealthBar;

    public GameObject deathCountObject;
    private TMP_Text deathCountText;

    [SerializeField] private AudioSource checkpointSound;

    [SerializeField] private AudioSource playerHurtSound;
    [SerializeField] private AudioSource playerDeadSound;

    [SerializeField] private AudioSource ghostHurtSound;
    [SerializeField] private AudioSource ghostDeadSound;

    private void Awake()
    {
        
        currentHealth = startingHealth;
        anim = GetComponent<Animator>();
        spriteRend = GetComponent<SpriteRenderer>();
        deathCountText = deathCountObject.GetComponent<TextMeshProUGUI>();


        if((PlayerPrefs.GetInt("deathCount") == null)) 
        {
            PlayerPrefs.SetInt("deathCount", 0);
        }
        else
        {
            PlayerPrefs.SetInt("deathCount", PlayerPrefs.GetInt("deathCount"));
        }
        
        deathCountText.text = "DEATH COUNT: " + PlayerPrefs.GetInt("deathCount");
    }

    private void Update() 
    {
        // Kill the player if they fall too deep
        if (transform.position.y < -10)
        {
            TakeDamage(999);
        }

    }

    public void TakeDamage(float _damage)
    {
        if (invulnerable) return;
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
        Physics2D.IgnoreLayerCollision(10, 11, true);
        for (int i = 0; i < numberOfFlashes; i++)
        {
            spriteRend.color = new Color(1, 0, 0, 0.5f);
            yield return new WaitForSeconds(iFramesDuration / (numberOfFlashes * 2));
            spriteRend.color = Color.white;
            yield return new WaitForSeconds(iFramesDuration / (numberOfFlashes * 2));
        }
        Physics2D.IgnoreLayerCollision(10, 11, false);
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
        StartCoroutine(Invunerability());
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

}