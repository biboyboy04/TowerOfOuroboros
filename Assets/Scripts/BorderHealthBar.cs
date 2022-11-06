using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BorderHealthBar : MonoBehaviour
{

    public static Animator anim;
   // private bool dead;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }


    // Update is called once per frame
    void Update()
    {
        if(Health.playerDead)
        {
           anim.SetTrigger("die");
        } 
        else
        {
            anim.SetTrigger("alive");
        }
    }
}
