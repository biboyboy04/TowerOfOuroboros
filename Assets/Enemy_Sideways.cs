using UnityEngine;

public class Enemy_Sideways : MonoBehaviour
{
    [SerializeField] private float movementDistance;
    [SerializeField] private float speed;
    [SerializeField] private float damage;
    [SerializeField] private bool vertical;
    private bool movingLeft;
    private float leftEdge;
    private float rightEdge;

    private bool movingTop;
    private float topEdge;
    private float bottomEdge;

    private void Awake()
    {
        leftEdge = transform.position.x - movementDistance;
        rightEdge = transform.position.x + movementDistance;

        topEdge = transform.position.y - movementDistance;
        bottomEdge = transform.position.y + movementDistance;
    }

    private void Update()
    {
        if (!vertical)
        {
            if (movingLeft)
            {
                if (transform.position.x > leftEdge)
                {
                    transform.position = new Vector3(transform.position.x - speed * Time.deltaTime, transform.position.y, transform.position.z);
                }
                else
                    movingLeft = false;
             }
            else
            {
                if (transform.position.x < rightEdge)
                {
                    transform.position = new Vector3(transform.position.x + speed * Time.deltaTime, transform.position.y, transform.position.z);
                }
                else
                    movingLeft = true;
            }
        }
        else 
        {
            if (movingTop)
            {
                if (transform.position.y > topEdge)
                {
                    transform.position = new Vector3(transform.position.x, transform.position.y - speed * Time.deltaTime, transform.position.z);
                }
                else
                    movingTop = false;
            }
            else
            {
                if (transform.position.y < bottomEdge)
                {
                    transform.position = new Vector3(transform.position.x, transform.position.y + speed * Time.deltaTime, transform.position.z);
                }
                else
                    movingTop = true;
            }
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            collision.GetComponent<Health>().TakeDamage(damage);
        }
    }
}