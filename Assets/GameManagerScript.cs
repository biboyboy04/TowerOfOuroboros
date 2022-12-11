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

    public static bool isPaused = false;

    [SerializeField] private AudioSource pauseSound;

    // Start is called before the first frame update
    void Start()
    {
        Health.playerDead = false;
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
        StartCoroutine(LoadAsynchronously(SceneManager.GetActiveScene().buildIndex));
    }

    public void respawn()
    {
        health.RespawnToCheckpoint();
        gameOverUI.SetActive(false);
        
    }

    public void pause()
    {
        // If the game is paused, unpause it
        if (isPaused)
        {
            UnpauseGame();
        }
        // Otherwise, pause the game
        else
        {
            PauseGame();
        }
    }

    void PauseGame()
    {
        // Set the isPaused flag to true
        isPaused = true;

        // Enable the pause menu
        pauseUI.SetActive(true);

        // Freeze time
      //  Time.timeScale = 0f;
    }

    // Unpause the game
    void UnpauseGame()
    {
        // Set the isPaused flag to false
        isPaused = false;

        // Disable the pause menu
        pauseUI.SetActive(false);

        // Unfreeze time
       // Time.timeScale = 1f;
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
