using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonDisable : MonoBehaviour
{

    public PlayerMovement playerMovement;
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
        if(button.name == "DashButton" &&  DashCheck())
        {
            button.interactable = true;
            image.color = Color.white;
            
        }

        else if(button.name == "AttackButton" &&  AttackCheck())
        {
            button.interactable = true;
            image.color = Color.white;
            
        }

        else
        {
            button.interactable = false;
            image.color = new Color32(55, 55, 55, 255);
        }
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
