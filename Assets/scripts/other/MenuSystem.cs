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
    private TroopsAndTowers troopsAndTowers;
    public float animationTime = 0.5f;
    private GameObject NeutralPosition;

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
        // get troops and towers script
        troopsAndTowers = GetComponent<TroopsAndTowers>();

        AddElements(troopsAndTowers.towerPrefabs);

        //get the child object called "Neutral position" in the selection menu
        NeutralPosition = selectionMenu.transform.Find("Neutral position").gameObject;
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

    /// <summary>
    /// Changes the active panel based on the given direction vector.
    /// </summary>
    /// <param name="direction">A Vector2 indicating the direction the player is pointing in. 
    /// The direction vector will be normalized and rounded to the nearest integer.</param>
    /// <remarks>
    /// The direction vector can be one of the following:
    /// - (0, 1) for up
    /// - (1, 1) for up-right
    /// - (1, 0) for right
    /// - (1, -1) for down-right
    /// - (0, -1) for down
    /// - (-1, -1) for down-left
    /// - (-1, 0) for left
    /// - (-1, 1) for up-left
    /// </remarks>
    public void ChangePanel(Vector2 direction)
    {
        AddElements(troopsAndTowers.towerPrefabs);
        int panel = -1;
        //normalize the direction vector
        direction = new Vector2(Mathf.Round(direction.x), Mathf.Round(direction.y));

        if (direction == Vector2.up)
            panel = 0;
        else if (direction == new Vector2(1, 1))
            panel = 1;
        else if (direction == Vector2.right)
            panel = 2;
        else if (direction == new Vector2(1, -1))
            panel = 3;
        else if (direction == Vector2.down)
            panel = 4;
        else if (direction == new Vector2(-1, -1))
            panel = 5;
        else if (direction == Vector2.left)
            panel = 6;
        else if (direction == new Vector2(-1, 1))
            panel = 7;
        else
            panel = 8;
        //if not 8 than deactivate 8
        if (panel != 8)
        {
            panels[8].transform.Find("outline").gameObject.SetActive(false);
        }

        if (panel != -1)
        {
            //turn off the outline of the current panel
            panels[activePanel].transform.Find("outline").gameObject.SetActive(false);
            //turn on the outline of the new panel
            panels[panel].transform.Find("outline").gameObject.SetActive(true);
            //set the new panel as the active panel
            activePanel = panel;
        }
    }

    public void AddElements(List<GameObject> elements)
    {
        for (int i = 0; i < elements.Count; i++)
        {
            if (i >= panels.Length)
                break;

            GameObject panel = panels[i];
            Transform objectImageTransform = panel.transform.Find("object image");

            if (objectImageTransform != null)
            {
                GameObject objectImage = objectImageTransform.gameObject;
                SpriteRenderer spriteRenderer = objectImage.GetComponent<SpriteRenderer>();

                if (spriteRenderer != null)
                {
                    spriteRenderer.sprite = elements[i].GetComponent<SpriteRenderer>().sprite;
                    spriteRenderer.sortingOrder = panel.GetComponent<SpriteRenderer>().sortingOrder + 1;
                    objectImage.transform.rotation = Quaternion.Euler(0, 0, 0);
                }
            }
        }
    }
}
