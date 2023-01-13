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
        if(health)
        {
            slider.maxValue = health.currentHealth;
            fill.color = gradient.Evaluate(1f);
        }

        else if (playerSoul)
        {
            slider.maxValue = 100;
        }

    }

    private void Update()
    {
        Debug.Log(slider.value);
        if(health)
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

        else if (playerSoul)
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