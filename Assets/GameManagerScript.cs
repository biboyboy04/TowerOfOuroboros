using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManagerScript : MonoBehaviour
{
    public GameObject gameOverUI;
    public Health health;
    public BorderHealthBar borderHealthBar;
    // Start is called before the first frame update
    void Start()
    {
        Health.playerDead = false;
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
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void respawn()
    {
        health.RespawnToCheckpoint();
        gameOverUI.SetActive(false);
        
    }

    public void menu()
    {
        SceneManager.LoadScene("GameMenu");
    }
}
