using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class FloorLevelText : MonoBehaviour
{
    public GameObject gameObject;
    public TMP_Text floorLevelText;
    public Dialogue dialogue;
    private Animator anim;
    private string floorName;
    private int currentFloor;
    private bool floorTextDisplayed;
    public bool noDialogue;
    

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        floorTextDisplayed  = false;
    }

    // Update is called once per frame
    void Update()
    {
       if((noDialogue || dialogue.isDialogueFinished) && !floorTextDisplayed )
        {
            floorName =  SceneManager.GetActiveScene().name;
            switch (floorName)
            {
                case "Miniboss1":
                    floorLevelText.text = "GHOST KING";
                    break;
                case "Miniboss2":
                    floorLevelText.text = "MAD WRAITH";
                    break;
                case "Miniboss3":
                    floorLevelText.text = "MUD TITAN";
                    break;
                case "FinalBoss":
                    floorLevelText.text = "OUROBOROS(GREG)";
                    break;
                default:
                    currentFloor = System.Int32.Parse(floorName.Substring(floorName.Length - 1));
                    floorLevelText.text = "FLOOR " + currentFloor;
                    break;
            }

            floorTextDisplayed  = true;
            anim.SetTrigger("displayText");
        }
    }
}
