using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine;
using System.ComponentModel.Design;

public class MenuSystem : MonoBehaviour
{   //reference to the selection menu
    public GameObject selectionMenu;
    //list of the 8 panels of the selection menu
    public GameObject[] panels;
    public DynamicGridSelector dynamicGridSelector;
    public InputActionReference openMenu;
    public int activePanel = 0;

    void Start()
    {
        //hide the selection menu
        selectionMenu.SetActive(false);

        //get all the panels in the selection menu
        panels = new GameObject[selectionMenu.transform.childCount];
        for (int i = 0; i < selectionMenu.transform.childCount; i++)
        {
            panels[i] = selectionMenu.transform.GetChild(i).gameObject;
        }


    }

    void Update()
    {
        //if openMenu is held down, show the selection menu
        if (openMenu.action.triggered)
        {
            //turn off the dynamic grid selector
            dynamicGridSelector.enabled = false;
            selectionMenu.SetActive(true);
        }

        if (openMenu.action.WasReleasedThisFrame())
        {
            //turn on the dynamic grid selector
            dynamicGridSelector.enabled = true;
            selectionMenu.SetActive(false);
        }
        //if enter is pressed, change the panel
        if (Keyboard.current.enterKey.wasPressedThisFrame)
        {
            ChangePanel((activePanel + 1) % panels.Length);
        }
    }

    public void ChangePanel(int panel)
    {
        //turn off the outline of the current panel
        panels[activePanel].transform.Find("outline").gameObject.SetActive(false);
        //turn on the outline of the new panel
        panels[panel].transform.Find("outline").gameObject.SetActive(true);
        //set the new panel as the active panel
        activePanel = panel;
    }
}
