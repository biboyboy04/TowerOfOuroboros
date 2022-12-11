using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class loadNextLevel : MonoBehaviour
{
    public int iLevelToLoad;
    public string sLevelToLoad;

    public int nextFloorNumber;

    public bool useIntegerToLoadLevel = false;

    public GameObject loadingScreen;
    public Slider slider;

    //public Text progressText;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision) 
    {
        if (collision.tag == "Player")
        {   
            PlayerPrefs.SetInt("levelReached", nextFloorNumber);
            Debug.Log("nextfloor" + nextFloorNumber);
            StartCoroutine(LoadAsynchronously());
        }
    }

    IEnumerator LoadAsynchronously ()
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sLevelToLoad);
        loadingScreen.SetActive(true);

        while (!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress / .9f);
            slider.value = progress;
            yield return null;
        }
    }

}
