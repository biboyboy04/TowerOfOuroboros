using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDrops : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject itemToDrop;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(this.GetComponent<Health>().currentHealth < 0)
        {
            DropItem();
        }
    }

    void DropItem()
    {
        if (itemToDrop != null)
        {
            // Spawn the item at the enemy's position
            Instantiate(itemToDrop, transform.position, Quaternion.identity);
        }
    }

}
