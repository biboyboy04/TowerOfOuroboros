using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManagerScript : MonoBehaviour
{
    public GameObject gameOverUI;
    public GameObject pauseUI;
    public Health health;
    public BorderHealthBar borderHealthBar;

    private bool isPaused;

    // Start is called before the first frame update
    void Start()
    {
        Health.playerDead = false;
        isPaused = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void gameOver()
    {
        gameOverUI.SetActive(true);
    }

    public void restart()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void respawn()
    {
        health.RespawnToCheckpoint();
        gameOverUI.SetActive(false);
        
    }

    public void pause()
    {
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


    public void menu()
    {
        SceneManager.LoadScene("GameMenu");
    }
}
