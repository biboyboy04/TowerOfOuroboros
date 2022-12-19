using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddSouls : MonoBehaviour
{
    [SerializeField] private float soulValue;
   // [SerializeField] private AudioClip pickupSound;
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            //SoundManager.instance.PlaySound(pickupSound);
            collision.GetComponent<Souls>().AddSouls(soulValue);
            gameObject.SetActive(false);
        }
    }
}
