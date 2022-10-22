using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFollow : MonoBehaviour
{
    public GameObject player;
    public float speed;
    private SpriteRenderer spriteRend;
    private Color originColor;


    private float distance;
    // Start is called before the first frame update
    void Start()
    {
        
        spriteRend = GetComponent<SpriteRenderer>();
    //    originColor = spriteRend.Color;

    }

    // Update is called once per frame
    void Update()
    {
        
        distance = Vector2.Distance(transform.position, player.transform.position);
        Vector2 direction = player.transform.position - transform.position;
        direction.Normalize();

        Vector2 playerPosition = new Vector2(player.transform.position.x, player.transform.position.y - 0.5f);

        if (distance < 10)
        {
            
            transform.position = Vector2.MoveTowards(this.transform.position, playerPosition, speed * Time.deltaTime);
            spriteRend.color = new Color(1, 0, 0, 0.5f);
            spriteRend.color = Color.red;
        }
        else 
        {
             spriteRend.color = Color.white;
        }
    }
}