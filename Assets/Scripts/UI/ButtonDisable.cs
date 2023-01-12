using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonDisable : MonoBehaviour
{
    [Header("Reference for Dash")]
    public PlayerMovement playerMovement;
    [Header("Reference for Attack")]
    public PlayerCombat playerCombat;
    
    public Button button;
    private Image image;
    private bool isInteractable;

    // Start is called before the first frame update
    void Start()
    {   
        button = GetComponent<Button>();
        image = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {

        if(button.name == "DashButton")
        {
            isInteractable = DashCheck();
        }

        else if(button.name == "AttackButton")
        {
            isInteractable = AttackCheck();
        }

        // Make the button interactable based on their checks
        button.interactable = isInteractable;

        // Make the button darker if it's not interactable otherwise do not tint the image
        image.color = isInteractable ? Color.white : new Color32(55, 55, 55, 255);
    }

    bool DashCheck()
    {
        return playerMovement.CanDash();
    }

    bool AttackCheck()
    {
        return playerCombat.CanAttack();
    }


}
