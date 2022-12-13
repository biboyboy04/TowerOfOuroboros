using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManagerScript : MonoBehaviour
{
    public GameObject gameOverUI;
    public GameObject pauseUI;
    public Health health;
    public BorderHealthBar borderHealthBar;
    public GameObject loadingScreen;
    public Slider slider;

    public bool canPause = true;

    [SerializeField] private AudioSource pauseSound;

    // Start is called before the first frame update
    void Start()
    {
        // To make camera follow and healthbar animation work if the player restart while dead
        Health.playerDead = false;
    }

    // StartMenu
    public void menu()
    {
        Time.timeScale = 1f;
        StartCoroutine(LoadAsynchronously(1));
        
    }

    // GameMenu
    public void NewGame()
    {
        PlayerPrefs.DeleteAll();
        PlayerPrefs.SetFloat("soulCount", 0);
        StartCoroutine(LoadAsynchronously(3));
    }

    public void ContinueGame()
    {
        StartCoroutine(LoadAsynchronously(2));
    }

    public void QuitGame()
    {
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
        Time.timeScale = 1f;
        PlayerPrefs.SetFloat("soulCount", PlayerPrefs.GetFloat("soulCountStart"));
        StartCoroutine(LoadAsynchronously(SceneManager.GetActiveScene().buildIndex));
    }

    public void respawn()
    {
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
