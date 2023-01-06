using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Souls : MonoBehaviour
{
    [SerializeField] public float startingSouls;
    public float currentSouls { get; private set; }
    public GameObject[] lights;
    public PlayerCombat playerCombat;
    private int soulUpgradeCount;
    private float playerDamage;
    public Color swordColor = new Color(0.1490196f, 0.8941177f, 1f, 1f);

    Color soulUpgrade1 = new Color(1, 0, 0, 0.5f);//red
    Color soulUpgrade2 = new Color(0, 1, 0, 0.5f);//green
    Color soulUpgrade3 = new Color(0, 0, 1, 0.5f);//blue
    Color soulUpgrade4 = new Color(1, 0, 1, 0.5f);//purple
    Color soulUpgrade5 = new Color(1, 1, 0, 0.5f);//yellow


    // Start is called before the first frame update
    void Start()
    {
        if (!PlayerPrefs.HasKey("soulCount"))
        {
            PlayerPrefs.SetFloat("soulCount", 0);
        }

        if (!PlayerPrefs.HasKey("soulUpgradeCount"))
        {
            PlayerPrefs.SetInt("soulUpgradeCount", 0);
        }

        // Get the player's combat script
        playerCombat = GetComponent<PlayerCombat>();

        // Change the color of the sword based on the number of soul upgrades
        ChangeWeaponColor();

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void AddSouls(float _value)
    {
        PlayerPrefs.SetFloat("soulCount", PlayerPrefs.GetFloat("soulCount")+_value);
        Debug.Log("Added "+_value);

        if(PlayerPrefs.GetFloat("soulCount") >= 100)
        {
            SoulUpgrade();
        }

    }

    public void SoulUpgrade()
    {
        // Reset the current number of souls to 0 and update the value in PlayerPrefs
        currentSouls = 0;
        PlayerPrefs.SetFloat("soulCount", currentSouls);
        
        soulUpgradeCount = PlayerPrefs.GetInt("souldUpgradeCount");
        playerDamage = PlayerPrefs.GetFloat("playerDamage", 10f);

        PlayerPrefs.SetInt("souldUpgradeCount", soulUpgradeCount + 1);
        PlayerPrefs.SetFloat("playerDamage", playerDamage + 10f);

        StartCoroutine(AnimateColor());
    }

    private IEnumerator AnimateColor()
    {

        int duration = 3;
        float elapsedTime = 0f;

        
        while (elapsedTime < duration)
        {
            float t = elapsedTime / duration;

            switch (PlayerPrefs.GetInt("souldUpgradeCount"))
            {
                case 1:
                    swordColor = soulUpgrade1;
                    break;
                case 2:
                    swordColor = soulUpgrade2;
                    break;
                case 3:
                    swordColor = soulUpgrade3;
                    break;
                case 4:
                    swordColor = soulUpgrade4;
                    break;
                case 5:
                    swordColor = soulUpgrade5;
                    break;
                default:
                    swordColor = soulUpgrade5;
                    break;
            }
            
            // Gradually Change sword's previous color to the current one
            lights[0].GetComponent<UnityEngine.Rendering.Universal.Light2D>().color = Color.Lerp(
            lights[0].GetComponent<UnityEngine.Rendering.Universal.Light2D>().color, 
            swordColor, t);

            lights[1].GetComponent<UnityEngine.Rendering.Universal.Light2D>().color = Color.Lerp(
            lights[1].GetComponent<UnityEngine.Rendering.Universal.Light2D>().color, 
           swordColor, t);

            lights[2].GetComponent<UnityEngine.Rendering.Universal.Light2D>().color = Color.Lerp(
            lights[2].GetComponent<UnityEngine.Rendering.Universal.Light2D>().color, 
           swordColor, t);

            lights[3].GetComponent<UnityEngine.Rendering.Universal.Light2D>().color = Color.Lerp(
            lights[3].GetComponent<UnityEngine.Rendering.Universal.Light2D>().color, 
            swordColor, t);

            // Levelup effect
            // Circular light around the player that increments
            lights[4].GetComponent<UnityEngine.Rendering.Universal.Light2D>().intensity=1.5f;
            lights[4].GetComponent<UnityEngine.Rendering.Universal.Light2D>().pointLightOuterRadius += 0.02f;
        

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        ChangeWeaponColor();
        // Remove the circular light around the player after the duration
        lights[4].GetComponent<UnityEngine.Rendering.Universal.Light2D>().intensity=0;
        lights[4].GetComponent<UnityEngine.Rendering.Universal.Light2D>().pointLightOuterRadius = 0.82f;
    }

    void ChangeWeaponColor()
    {
        switch (PlayerPrefs.GetInt("souldUpgradeCount"))
        {
            case 0:
                swordColor = new Color(0.1490196f, 0.8941177f, 1f, 1f);
                break;
            case 1:
                swordColor = soulUpgrade1;
                break;
            case 2:
                swordColor = soulUpgrade2;
                break;
            case 3:
                swordColor = soulUpgrade3;
                break;
            case 4:
                swordColor = soulUpgrade4;
                break;
            case 5:
                swordColor = soulUpgrade5;
                break;
            default:
                swordColor = soulUpgrade5;
                break;
        }

        lights[0].GetComponent<UnityEngine.Rendering.Universal.Light2D>().color = swordColor;
        lights[1].GetComponent<UnityEngine.Rendering.Universal.Light2D>().color = swordColor;
        lights[2].GetComponent<UnityEngine.Rendering.Universal.Light2D>().color = swordColor;
        lights[3].GetComponent<UnityEngine.Rendering.Universal.Light2D>().color = swordColor;
    }
}
