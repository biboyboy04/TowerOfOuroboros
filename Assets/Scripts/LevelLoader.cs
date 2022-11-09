using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelLoader : MonoBehaviour
{
    public GameObject loadingScreen;
    public Slider slider;

    public void LoadLevel(string scene)
    {
        StartCoroutine(AsynchronousLoad(scene));
    }

    IEnumerator AsynchronousLoad (string scene)
    {
        yield return null;
        loadingScreen.SetActive(true);
        AsyncOperation operation = SceneManager.LoadSceneAsync(scene);
        //loading

        while (!operation.isDone)
        {
            // [0, 0.9] > [0, 1]
            float progress = Mathf.Clamp01(operation.progress / .9f);
            slider.value = progress;
            Debug.Log("Loading progress: " + (progress * 100) + "%");
            yield return null;
        }
    }
}
