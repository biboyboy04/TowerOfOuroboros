using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StickToPlatform : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter2D(Collision2D coll)
    {
        if(coll.gameObject.tag == "Platform")
        {
            transform.parent = coll.gameObject.transform;
        }
    }
    void OnCollisionExit2D(Collision2D coll)
    {
        if(coll.gameObject.tag == "Platform")
        {
            transform.parent = null;
        }
    }
}
