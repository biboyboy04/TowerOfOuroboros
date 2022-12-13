using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Souls : MonoBehaviour
{
    [SerializeField] public float startingSouls;
    public float currentSouls { get; private set; }
    public GameObject[] lights;
    public PlayerCombat playerCombat;

    // Start is called before the first frame update
    void Start()
    {
        if((PlayerPrefs.GetFloat("soulCount") == null)) 
        {
            PlayerPrefs.SetFloat("soulCount", 0);
        }
        playerCombat = GetComponent<PlayerCombat>();

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void AddSouls(float _value)
    {
        PlayerPrefs.SetFloat("soulCount", PlayerPrefs.GetFloat("soulCount")+_value);

        if(PlayerPrefs.GetFloat("soulCount") >= 100)
        {
            SoulUpgrade();
        }

    }

    public void SoulUpgrade()
    {
        currentSouls = 0;
        PlayerPrefs.SetFloat("soulCount", currentSouls);
        //do something
        playerCombat.damage *= 1.25f;
        StartCoroutine(AnimateColor());
        //foreach (GameObject light in lights) { light.GetComponent<UnityEngine.Rendering.Universal.Light2D>().color = new Color(1, 0, 0, 0.5f); }
    }

    private IEnumerator AnimateColor()
    {
        int duration = 5;
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            float t = elapsedTime / duration;
            lights[0].GetComponent<UnityEngine.Rendering.Universal.Light2D>().color = Color.Lerp(
            lights[0].GetComponent<UnityEngine.Rendering.Universal.Light2D>().color, 
            new Color(1, 0, 0, 0.5f), t);

            lights[1].GetComponent<UnityEngine.Rendering.Universal.Light2D>().color = Color.Lerp(
            lights[1].GetComponent<UnityEngine.Rendering.Universal.Light2D>().color, 
            new Color(1, 0, 0, 0.5f), t);

            lights[2].GetComponent<UnityEngine.Rendering.Universal.Light2D>().color = Color.Lerp(
            lights[2].GetComponent<UnityEngine.Rendering.Universal.Light2D>().color, 
            new Color(1, 0, 0, 0.5f), t);

            lights[3].GetComponent<UnityEngine.Rendering.Universal.Light2D>().color = Color.Lerp(
            lights[3].GetComponent<UnityEngine.Rendering.Universal.Light2D>().color, 
            new Color(1, 0, 0, 0.5f), t);

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        lights[0].GetComponent<UnityEngine.Rendering.Universal.Light2D>().color = new Color(1, 0, 0, 0.5f);
        lights[1].GetComponent<UnityEngine.Rendering.Universal.Light2D>().color = new Color(1, 0, 0, 0.5f);
        lights[2].GetComponent<UnityEngine.Rendering.Universal.Light2D>().color = new Color(1, 0, 0, 0.5f);
        lights[3].GetComponent<UnityEngine.Rendering.Universal.Light2D>().color = new Color(1, 0, 0, 0.5f);
    }
}
