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
    private bool poppedUp;
    public bool noDialogue;
    

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        poppedUp = false;
    }

    // Update is called once per frame
    void Update()
    {
        if((noDialogue == true || dialogue.isDialogueFinished) && !poppedUp)
        {
            //Change this later
            floorName =  SceneManager.GetActiveScene().name;
            if(floorName == "Miniboss1")
            {
                floorLevelText.text = "GHOST KING";
            }
            else if(floorName == "Miniboss2")
            {
                floorLevelText.text = "MAD WRAITH";
            }
            else if(floorName == "Miniboss3")
            {
                floorLevelText.text = "MUD TITAN";
            }
            else if(floorName == "FinalBoss")
            {
                floorLevelText.text = "Greg(OUROBOROS)";
            }
            else
            {
                currentFloor = System.Int32.Parse(floorName.Substring(floorName.Length - 1));
                floorLevelText.text = "FLOOR " + currentFloor;
            }
            poppedUp = true;
            anim.SetTrigger("expand");
        }
    }
}
