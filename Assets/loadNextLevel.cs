using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class loadNextLevel : MonoBehaviour
{
    public int iLevelToLoad;
    public string sLevelToLoad;

    public bool useIntegerToLoadLevel = false;

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
            Invoke("LoadScene", 0.5f);
        }
    }

    void LoadScene()
    {
        if(useIntegerToLoadLevel)
        {
            SceneManager.LoadScene(iLevelToLoad);
        }
        else 
        {
            SceneManager.LoadScene(sLevelToLoad);
        }
    }
}
