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
    Color soulUpgrade5 = new Color(1, 1, 0, 0.5f);//whiet


    // Start is called before the first frame update
    void Start()
    {
        if((PlayerPrefs.GetFloat("soulCount") == null)) 
        {
            PlayerPrefs.SetFloat("soulCount", 0);
        }
        playerCombat = GetComponent<PlayerCombat>();

        if((PlayerPrefs.GetInt("souldUpgradeCount") == null)) 
        {
            PlayerPrefs.SetInt("souldUpgradeCount", 0);
        }

        ChangeWeaponColor();

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void AddSouls(float _value)
    {
        PlayerPrefs.SetFloat("soulCount", PlayerPrefs.GetFloat("soulCount")+_value);
        Debug.Log("ADded"+_value);

        if(PlayerPrefs.GetFloat("soulCount") >= 100)
        {
            SoulUpgrade();
        }

    }

    public void SoulUpgrade()
    {
        currentSouls = 0;
        PlayerPrefs.SetFloat("soulCount", currentSouls);

        soulUpgradeCount = PlayerPrefs.GetInt("souldUpgradeCount");
        playerDamage = PlayerPrefs.GetFloat("playerDamage", 10f);

        Debug.Log("Player soulcount"+  PlayerPrefs.GetInt("souldUpgradeCount"));
        PlayerPrefs.SetInt("souldUpgradeCount", soulUpgradeCount + 1);
        Debug.Log("Player soulcount"+  PlayerPrefs.GetInt("souldUpgradeCount"));
        Debug.Log("Player damage"+ PlayerPrefs.GetFloat("playerDamage", 10f));
        PlayerPrefs.SetFloat("playerDamage", playerDamage + 10f);
        Debug.Log("Player damage"+ PlayerPrefs.GetFloat("playerDamage", 10f));
        StartCoroutine(AnimateColor());
        //foreach (GameObject light in lights) { light.GetComponent<UnityEngine.Rendering.Universal.Light2D>().color = new Color(1, 0, 0, 0.5f); }
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
                    swordColor = new Color(0.1490196f, 0.8941177f, 1f, 1f);
                    break;
            }

            lights[4].GetComponent<UnityEngine.Rendering.Universal.Light2D>().intensity=1.5f;
            lights[4].GetComponent<UnityEngine.Rendering.Universal.Light2D>().pointLightOuterRadius += 0.02f; 
            
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

            lights[4].GetComponent<UnityEngine.Rendering.Universal.Light2D>().intensity=1.5f;
            lights[4].GetComponent<UnityEngine.Rendering.Universal.Light2D>().pointLightOuterRadius += 0.02f;
        

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        ChangeWeaponColor();
       
        lights[4].GetComponent<UnityEngine.Rendering.Universal.Light2D>().intensity=0;
        lights[4].GetComponent<UnityEngine.Rendering.Universal.Light2D>().pointLightOuterRadius = 0.82f;
       // lights[4].GetComponent<UnityEngine.Rendering.Universal.Light2D>().falloffIntensity=0;
    }

    void ChangeWeaponColor()
    {
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
                swordColor = new Color(0.1490196f, 0.8941177f, 1f, 1f);
                break;
        }

        lights[0].GetComponent<UnityEngine.Rendering.Universal.Light2D>().color = swordColor;
        lights[1].GetComponent<UnityEngine.Rendering.Universal.Light2D>().color = swordColor;
        lights[2].GetComponent<UnityEngine.Rendering.Universal.Light2D>().color = swordColor;
        lights[3].GetComponent<UnityEngine.Rendering.Universal.Light2D>().color = swordColor;
    }
}
