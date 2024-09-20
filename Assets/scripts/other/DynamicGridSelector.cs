using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class DynamicGridSelector : MonoBehaviour
{
    public bool blueTeam;
    public string tileTag;
    public GameObject selectionPrefab; // Reference to the selection indicator GameObject
    public GameObject selection;
    public float maxX = 10f;     // Maximum X boundary
    public float minX = 0f;      // Minimum X boundary
    public float maxY = 10f;     // Maximum Y boundary
    public float minY = 0f;      // Minimum Y boundary
    public float inputDelay = 0.5f; // Delay to prevent rapid selection changes
    private float inputTimer;
    public InputActionReference movep1;
    public InputActionReference movep2;
    public InputActionReference buyAction; // Input action for purchasing turrets
    private InputActionReference move;

    // Initial position of the player
    [SerializeField]
    private Vector2 currentPosition = new Vector2(1.5f, 5.5f);

    // Reference to the last selected object
    private GameObject lastSelectedObject;

    void Start()
    {
        // Initialize the selection GameObject at the starting position
        if (selectionPrefab != null)
        {
            selection = Instantiate(selectionPrefab, currentPosition, Quaternion.identity);
        }
        UpdateSelection(); // Update selection state at the start
        buyAction.action.performed += OnBuyActionPerformed; // Subscribe to the purchase input action
    }

    void Update()
    {
        if (blueTeam)
            move = movep1;
        else    
            move = movep2;
               
        inputTimer -= Time.deltaTime;

        // Read joystick input
        Vector2 movedirection = move.action.ReadValue<Vector2>();
        float horizontal = movedirection.x;
        float vertical = movedirection.y;
        
        // Normalize input to move only in one direction at a time
        if (Mathf.Abs(horizontal) > Mathf.Abs(vertical))
        {
            vertical = 0; // Prioritize horizontal movement
        }
        else if (Mathf.Abs(vertical) > Mathf.Abs(horizontal))
        {
            horizontal = 0; // Prioritize vertical movement
        }
        else
            horizontal = 0; // Prioritize vertical movement if equal

        // Check for input and ensure there's a delay between moves
        if (inputTimer <= 0 && (horizontal != 0 || vertical != 0))
        {
            Vector2 direction = new Vector2(horizontal, vertical).normalized;
            MoveSelection(direction);
            inputTimer = inputDelay; // Reset the timer
        }

        if (horizontal == 0 && vertical == 0)
        {
            inputTimer = 0; // Reset to 0 to avoid underflow
        }
    }

    // Triggered when the purchase key is pressed
    private void OnBuyActionPerformed(InputAction.CallbackContext context)
    {
        if (lastSelectedObject != null)
        {
            var tileScript = lastSelectedObject.GetComponent<tiles>();
            if (tileScript != null && tileScript.hover && !tileScript.activeConstruction)
            {
                GameManager gameManager = tileScript.gameManager;

                // Check if the player can afford the turret
                if (!gameManager.pause && gameManager.currentCoins >= tileScript.weaponPrice)
                {
                    // Trigger turret construction and deduct currency
                    tileScript.activeConstruction = true;
                    gameManager.AddCoins(-tileScript.weaponPrice);
                    StartCoroutine(tileScript.SpawnObject(tileScript.turret1)); // Trigger construction

                    Debug.Log("Turret purchased and construction started!");
                }
                else
                {
                    Debug.Log("Not enough coins or game is paused.");
                }
            }
        }
    }

    void MoveSelection(Vector2 direction)
    {
        // Calculate the new position by moving one unit in the desired direction
        Vector2 newPosition = currentPosition + new Vector2(direction.x, direction.y);

        // Clamp the position to keep within boundaries
        newPosition.x = Mathf.Clamp(newPosition.x, minX, maxX);
        newPosition.y = Mathf.Clamp(newPosition.y, minY, maxY);

        // Update the current position and move the selection GameObject
        currentPosition = newPosition;
        if (selection != null)
        {
            selection.transform.position = currentPosition;
        }

        UpdateSelection();
    }

    void UpdateSelection()
    {
        // Raycast to check if we are over a tile
        RaycastHit2D hit = Physics2D.Raycast(currentPosition, Vector2.zero);

        // Deselect the previous object
        if (lastSelectedObject != null)
        {
            DeselectObject(lastSelectedObject);
            lastSelectedObject = null;
        }

        // Check if we hit a tile
        if (hit.collider != null)
        {
            GameObject hitObject = hit.collider.gameObject;

            if (hitObject.CompareTag(tileTag))
            {
                SelectObject(hitObject);
                lastSelectedObject = hitObject;
            }
        }
    }

    void SelectObject(GameObject obj)
    {
        // Check for the tile component and set the hover status to true
        var tileScript = obj.GetComponent<tiles>(); 
        if (tileScript != null)
        {
            tileScript.hover = true; // Set hover to true for selected tile
        }
    }

    void DeselectObject(GameObject obj)
    {
        // Check for the tile component and set the hover status to false
        var tileScript = obj.GetComponent<tiles>(); 
        if (tileScript != null)
        {
            tileScript.hover = false; // Set hover to false for deselected tile
        }
    }

    private void OnDestroy()
    {
        // Unsubscribe from the buy action event when this object is destroyed
        buyAction.action.performed -= OnBuyActionPerformed;
    }
}
