using UnityEngine;

public class PlayerRespawn : MonoBehaviour
{
    private Transform currentCheckpoint;
    private Health playerHealth;
    private SpriteRenderer spriteRend;

    [SerializeField] private AudioSource checkpointSound;

    private void Awake()
    {
        playerHealth = GetComponent<Health>();
        spriteRend = GetComponent<SpriteRenderer>();
    }

    private void Update()
    { 

    }

    public void Respawn()
    {
        playerHealth.Respawn(); //Restore player health and reset animation
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
            spriteRend.color = new Color(1, 0, 0, 0.5f);
            spriteRend.color = Color.red;
            collision.GetComponent<Collider2D>().enabled = false;

          // collision.GetComponent<Animator>().SetTrigger("activate");
        }
    }

}
