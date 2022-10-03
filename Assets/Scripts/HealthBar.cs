using UnityEngine;
using UnityEngine.UI;

public class Healthbar : MonoBehaviour
{
    [SerializeField] private Health playerHealth;
    //[SerializeField] private Image totalhealthBar;
    //[SerializeField] private Image currenthealthBar;
    [SerializeField] public Slider slider;
    [SerializeField] public Gradient gradient;
    public Image fill;

    private void Start()
    {
       // totalhealthBar.fillAmount = playerHealth.currentHealth;
        slider.maxValue = playerHealth.currentHealth;
		//slider.value = playerHealth.currentHealth;
        fill.color = gradient.Evaluate(1f);
    }
    private void Update()
    {
        //currenthealthBar.fillAmount = playerHealth.currentHealth;
        slider.value = playerHealth.currentHealth;
		fill.color = gradient.Evaluate(slider.normalizedValue);
    }
}