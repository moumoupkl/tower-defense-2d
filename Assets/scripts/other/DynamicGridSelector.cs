using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class DynamicGridSelector : MonoBehaviour
{
    public bool blueTeam;
    public string tileTag;
    public GameObject selectionPrefab;
    public GameObject selection;
    public float maxX = 10f;
    public float minX = 0f;
    public float maxY = 10f;
    public float minY = 0f;
    public float inputDelay = 0.5f;
    private float inputTimer;
    public InputActionReference movep1;
    public InputActionReference buyTurret1; // Input for turret 1
    public InputActionReference buyTurret2; // Input for turret 2
    private InputActionReference move;
    public GameManager gameManager;

    [SerializeField]
    private Vector2 currentPosition = new Vector2(1.5f, 5.5f);

    private GameObject lastSelectedObject;

    void Start()
    {
        Camera mainCamera = Camera.main;
        gameManager = mainCamera.GetComponent<GameManager>();

        if (selectionPrefab != null)
        {
            selection = Instantiate(selectionPrefab, currentPosition, Quaternion.identity);
        }
        UpdateSelection();

        // Subscribe to both turret purchase actions
        buyTurret1.action.performed += ctx => OnBuyActionPerformed(1); // For turret type 1
        buyTurret2.action.performed += ctx => OnBuyActionPerformed(2); // For turret type 2
    }

    void Update()
    {
        move = movep1;
        inputTimer -= Time.deltaTime;

        Vector2 movedirection = move.action.ReadValue<Vector2>();
        float horizontal = movedirection.x;
        float vertical = movedirection.y;

        if (Mathf.Abs(horizontal) > Mathf.Abs(vertical)) vertical = 0;
        else if (Mathf.Abs(vertical) > Mathf.Abs(horizontal)) horizontal = 0;

        if (inputTimer <= 0 && (horizontal != 0 || vertical != 0))
        {
            Vector2 direction = new Vector2(horizontal, vertical).normalized;
            MoveSelection(direction);
            inputTimer = inputDelay;
        }

        if (horizontal == 0 && vertical == 0) inputTimer = 0;

        // Check if the selector is out of bounds or misaligned and reset if necessary
        CheckAndCorrectSelectorPosition();
    }

    // Handle the turret purchase
    private void OnBuyActionPerformed(int turretType)
    {
        if (lastSelectedObject != null)
        {
            var tileScript = lastSelectedObject.GetComponent<tiles>();
            if (tileScript != null && tileScript.hover && !tileScript.activeConstruction)
            {
                int weaponPrice = 5;  // Adjust price for balance
                bool hasEnoughCoins = blueTeam ? gameManager.blueCoins >= weaponPrice : gameManager.redCoins >= weaponPrice;

                if (!gameManager.pause && hasEnoughCoins)
                {
                    tileScript.activeConstruction = true;
                    gameManager.AddCoins(-weaponPrice, blueTeam);

                    // Select the correct turret to spawn
                    GameObject turretToSpawn = turretType == 1 ? tileScript.turret1 : tileScript.turret2;
                    StartCoroutine(tileScript.SpawnObject(turretToSpawn));  // Start construction

                    Debug.Log($"Turret {turretType} purchased and construction started!");
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
        if (!gameManager.pause)
        {
            Vector2 newPosition = currentPosition + direction;
            newPosition.x = Mathf.Clamp(newPosition.x, minX, maxX);
            newPosition.y = Mathf.Clamp(newPosition.y, minY, maxY);

            currentPosition = newPosition;
            if (selection != null)
            {
                selection.transform.position = currentPosition;
            }

            UpdateSelection();
        }
    }

    void UpdateSelection()
    {
        RaycastHit2D hit = Physics2D.Raycast(currentPosition, Vector2.zero);

        if (lastSelectedObject != null)
        {
            DeselectObject(lastSelectedObject);
            lastSelectedObject = null;
        }

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
        var tileScript = obj.GetComponent<tiles>();
        if (tileScript != null)
        {
            tileScript.hover = true;
        }
    }

    void DeselectObject(GameObject obj)
    {
        var tileScript = obj.GetComponent<tiles>();
        if (tileScript != null)
        {
            tileScript.hover = false;
        }
    }

    // Check if the selector is misaligned or out of bounds and reset it
    void CheckAndCorrectSelectorPosition()
    {
        bool isMisaligned = (currentPosition.x % 1f != 0.5f || currentPosition.y % 1f != 0.5f);
        bool isOutOfBounds = (currentPosition.x < minX || currentPosition.x > maxX || currentPosition.y < minY || currentPosition.y > maxY);

        if (isMisaligned || isOutOfBounds)
        {
            Debug.Log("Selector misaligned or out of bounds, resetting position.");
            ResetSelectorPosition();
        }
    }

    // Reset the selector to the nearest valid grid position
    void ResetSelectorPosition()
    {
        float clampedX = Mathf.Clamp(currentPosition.x, minX, maxX);
        float clampedY = Mathf.Clamp(currentPosition.y, minY, maxY);

        // Snap the position to the nearest valid tile center (assuming tile centers are at 0.5 intervals)
        float snappedX = Mathf.Round(clampedX - 0.5f) + 0.5f;
        float snappedY = Mathf.Round(clampedY - 0.5f) + 0.5f;

        currentPosition = new Vector2(snappedX, snappedY);

        if (selection != null)
        {
            selection.transform.position = currentPosition;
        }

        UpdateSelection();
    }

    private void OnDestroy()
    {
        buyTurret1.action.performed -= ctx => OnBuyActionPerformed(1);
        buyTurret2.action.performed -= ctx => OnBuyActionPerformed(2);
    }
}
