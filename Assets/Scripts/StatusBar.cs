using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class StatusBar : MonoBehaviour
{
    [SerializeField] private Health health;
    [SerializeField] private Souls playerSoul;
    [SerializeField] public Slider slider;
    [SerializeField] public Gradient gradient;
    public Image fill;
    public GameObject bossRageText;
    bool notRaged = true;

    private void Start()
    {
        if(playerSoul == null)
        {
            slider.maxValue = health.currentHealth;
            //slider.value = health.currentHealth;
            fill.color = gradient.Evaluate(1f);
        }

        else if (health == null)
        {
            //slider.maxValue = playerSoul.currentSouls;
            //slider.value = health.currentHealth;
        }

    }

    private void Update()
    {
        if(health != null)
        {
            slider.value = health.currentHealth;
            fill.color = gradient.Evaluate(slider.normalizedValue);
            
            if(health.tag == "Boss")
            {   
                if(IsHalfHealth())
                {
                    notRaged = false;
                    bossRageText.SetActive(true);
                }
            }
        }

        else if (playerSoul != null)
        {
            slider.value = PlayerPrefs.GetFloat("soulCount");
        }

        
    }

     private bool IsHalfHealth()
    {   

        if(notRaged)
        {
            return health.currentHealth <= (health.startingHealth / 2);
        }
        else
            return false;
    }
}