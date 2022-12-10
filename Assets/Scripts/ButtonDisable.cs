using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonDisable : MonoBehaviour
{

    public PlayerMovement playerMovement;
    public Button button;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(playerMovement.CanDash())
        {
            button.interactable = true;
        }
        else
        {
            button.interactable = false;
        }
    }


}
