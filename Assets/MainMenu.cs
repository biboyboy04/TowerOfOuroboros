using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class MainMenu : MonoBehaviour
{
    public void NewGame()
    {
        PlayerPrefs.DeleteAll();
        SceneManager.LoadScene("Floor1");
    }

    public void ContinueGame()
    {
        PlayerPrefs.SetInt("levelReached", 7);
        SceneManager.LoadScene("LevelSelect");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
