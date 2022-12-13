using UnityEngine;
using UnityEngine.UI;

public class StatusBar : MonoBehaviour
{
    [SerializeField] private Health playerHealth;
    [SerializeField] private Souls playerSoul;
    [SerializeField] public Slider slider;
    [SerializeField] public Gradient gradient;
    public Image fill;

    private void Start()
    {
        if(playerSoul == null)
        {
            slider.maxValue = playerHealth.currentHealth;
            //slider.value = playerHealth.currentHealth;
            fill.color = gradient.Evaluate(1f);
        }

        else if (playerHealth == null)
        {
            //slider.maxValue = playerSoul.currentSouls;
            //slider.value = playerHealth.currentHealth;
        }

    }

    private void Update()
    {
        if(playerSoul == null)
        {
            slider.value = playerHealth.currentHealth;
            fill.color = gradient.Evaluate(slider.normalizedValue);
        }

        else if (playerHealth == null)
        {
            slider.value = PlayerPrefs.GetFloat("soulCount");
        }
    }
}