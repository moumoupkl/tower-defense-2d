using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine;

public class MenuSystem : MonoBehaviour
{   //reference to the selection menu
    public GameObject selectionMenu;
    //list of the 8 panels of the selection menu
    public GameObject[] panels;
    public DynamicGridSelector dynamicGridSelector;
    public InputActionReference openMenu;

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
        
    }
}
