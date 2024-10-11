using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine;

public class MenuSystem : MonoBehaviour
{   //reference to the selection menu
    public GameObject selectionMenu;
    //list of the 8 panels of the selection menu
    public GameObject[] panels;
    public InputActionReference openMenu;
    private bool open;

    void Start()
    {
        //hide the selection menu
        selectionMenu.SetActive(false);
        

        //populate the panels array with the x panels of the selection menu
        for (int i = 0; i < panels.Length; i++)
        {
            panels[i] = selectionMenu.transform.GetChild(i).gameObject;
        }

        
    }

    void Update()
    {
        //if the openMenu action is triggered, set open to true
        if (openMenu.action.triggered)
        {
            SetPanelRenderers(true);
            SetPlayerInput(true);
            open = true;
        }
        /*
        //if the openMenu action is let go, set open to false
        if (openMenu.action.phase == InputActionPhase.Canceled)
        {
            SetPanelRenderers(false);
            SetPlayerInput(false);
            open = false;
        }
        */
    }

    //function to set player_input bool of the animator to true/false for every panel
    public void SetPlayerInput(bool input)
    {
        for (int i = 0; i < panels.Length; i++)
        {
            panels[i].GetComponent<Animator>().SetBool("player_input", input);
        }
    }

    //function to activate/deactivate the renderers of the panels
    public void SetPanelRenderers(bool render)
    {
        for (int i = 0; i < panels.Length; i++)
        {
            panels[i].GetComponent<Renderer>().enabled = render;
        }
    }
}
