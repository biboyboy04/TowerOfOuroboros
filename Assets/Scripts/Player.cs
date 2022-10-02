using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

	public int maxHealth = 100;
	public int currentHealth;
    private Animator anim;
    public static bool isDead;
	public HealthBar healthBar;
    

    // Start is called before the first frame update
    void Start()
    {
		currentHealth = maxHealth;
		healthBar.SetMaxHealth(maxHealth);
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
		if (Input.GetKeyDown(KeyCode.E))
		{
			TakeDamage(20);
            anim.SetTrigger("hurt");
		}

        if (currentHealth > 0)
        {
            //anim.SetTrigger("hurt");
            //iframes
        }
        else
        {
            if (!isDead)
            {
                anim.SetTrigger("die");
                GetComponent<PlayerMovement>().enabled = false;
                isDead = true;
                Debug.Log(isDead);
            }
        }

    }

	void TakeDamage(int damage)
	{
		currentHealth -= damage;

		healthBar.SetHealth(currentHealth);

	}
}
