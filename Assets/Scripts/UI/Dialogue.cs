using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Dialogue : MonoBehaviour
{
    public TextMeshProUGUI textComponent;
    public string[] lines;
    public AudioClip[] audioClips;
    public AudioSource audioSource;
    public float textSpeed;
    public GameObject bg;
    public GameObject leanTouch;

    public bool isDialogueFinished = false;

    private int index;

    private void Start() 
    {
        textComponent.text = string.Empty;
        audioSource.clip = audioClips[0];
        StartDialogue();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void StartDialogue()
    {
        index = 0;
        PlayNextAudioClip(index);
        StartCoroutine(TypeLine());
    }

    IEnumerator TypeLine()
    {
        foreach (char c in lines[index].ToCharArray())
        {
            textComponent.text+= c;
            yield return new WaitForSeconds(textSpeed);
        }
    }

    public void NextLineInput()
    {
        if(textComponent.text == lines[index])
        {
            NextLine();
        }
        else
        {
            StopAllCoroutines();
            textComponent.text = lines[index];
        }
    }

    void NextLine()
    {
        if(index < lines.Length-1)
        {
            index++;
            PlayNextAudioClip(index);
            textComponent.text = string.Empty;
            StartCoroutine(TypeLine());
        }
        else
        {
            gameObject.SetActive(false);
            bg.SetActive(false);
            leanTouch.SetActive(false);
            isDialogueFinished = true;
            
        }
    }

    void PlayNextAudioClip(int index)
    {
        audioSource.Stop();
        audioSource.clip = audioClips[index];
        audioSource.Play();
    }
}
