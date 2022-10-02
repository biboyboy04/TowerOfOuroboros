using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BorderHealthBar : MonoBehaviour
{

    public static Animator anim;
    public bool dead;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }


    // Update is called once per frame
    void Update()
    {
        dead = Player.isDead;
        Debug.Log("Updating" + dead);
        if(dead)
        {
           anim.SetTrigger("die");
           dead = !dead;
        }
    }
}
