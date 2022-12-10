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

    private bool isPaused;

    [SerializeField] private AudioSource pauseSound;

    // Start is called before the first frame update
    void Start()
    {
        Health.playerDead = false;
        isPaused = true;
    }

    // StartMenu
    public void menu()
    {
        StartCoroutine(LoadAsynchronously(1));
    }


    // GameMenu
    public void NewGame()
    {
        PlayerPrefs.DeleteAll();
        StartCoroutine(LoadAsynchronously(3));
    }

    public void ContinueGame()
    {
        PlayerPrefs.SetInt("levelReached", 7);
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
        Time.timeScale = 1;
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
        
        if(isPaused)
        {
            isPaused = false;
            pauseUI.SetActive(true);
            Time.timeScale = 0;
        }
        else
        {
            isPaused = true;
            pauseUI.SetActive(false);
            Time.timeScale = 1;
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
