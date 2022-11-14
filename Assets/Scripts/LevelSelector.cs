using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelSelector : MonoBehaviour
{
    // Start is called before the first frame update
    
	public Button[] levelButtons;
    void Start()
    {
        int levelReached = PlayerPrefs.GetInt("levelReached", 1);

		for (int i = 0; i < levelButtons.Length; i++)
		{
			if (i + 1 > levelReached)
				levelButtons[i].interactable = false;
		}
    }

	public void Select (string levelName)
	{
        SceneManager.LoadScene(levelName);
    }

}
