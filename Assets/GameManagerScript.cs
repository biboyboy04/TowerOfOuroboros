using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManagerScript : MonoBehaviour
{
    public GameObject gameOverUI;
    public GameObject pauseUI;
    public GameObject newGameUI;
    public Health health;
    public BorderHealthBar borderHealthBar;
    public GameObject loadingScreen;
    public Slider slider;
    public AudioSource uiSound;

    public bool canPause = true;



    [SerializeField] private AudioSource pauseSound;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // StartMenu
    public void menu()
    {
        uiSound.Play();
        Time.timeScale = 1f;
        PlayerPrefs.SetFloat("soulCount", PlayerPrefs.GetFloat("soulCountStart"));
        StartCoroutine(LoadAsynchronously(1));
    }

    // GameMenu
    public void NewGame()
    {
        if(PlayerPrefs.GetInt("levelReached") == null || PlayerPrefs.GetInt("levelReached") == 0) 
        {
            StartNewGame();
        }
        else
        {
            newGameUI.SetActive(true);
        }
    }

    public void StartNewGame() 
    {
        uiSound.Play();
        PlayerPrefs.DeleteAll();
        PlayerPrefs.SetFloat("soulCount", 0);
        //PlayerPrefs.SetBool("isNewGame", true);
        StartCoroutine(LoadAsynchronously(3));
    }

    public void DoNotStartNewGame()
    {
        uiSound.Play();
        newGameUI.SetActive(false);
    }

    public void ContinueGame()
    {
        uiSound.Play();
        StartCoroutine(LoadAsynchronously(2));
    }

    public void QuitGame()
    {
        uiSound.Play();
        Application.Quit();
    }


    // Game
    // Cant refactor methods because i need to reference all the method to each buttons (tedious)
    public void gameOver()
    {
        gameOverUI.SetActive(true);
    }

    public void restart()
    {
        uiSound.Play();
        Time.timeScale = 1f;
        PlayerPrefs.SetFloat("soulCount", PlayerPrefs.GetFloat("soulCountStart"));
        StartCoroutine(LoadAsynchronously(SceneManager.GetActiveScene().buildIndex));
    }

    public void respawn()
    {
        uiSound.Play();
        health.RespawnToCheckpoint();
        gameOverUI.SetActive(false);
        
    }

    public void pause()
    {

        pauseSound.Play();

        if (canPause)
        {
            canPause = false;
            pauseUI.SetActive(true);
            Time.timeScale = 0f;
        }

        else
        {
            canPause = true;
            pauseUI.SetActive(false);
            Time.timeScale = 1f;
        }
    }


    IEnumerator LoadAsynchronously (int sceneToLoad)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneToLoad);
        loadingScreen.SetActive(true);

        while (!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress / .9f);
            slider.value = progress;
            yield return null;
        }
    }
}
