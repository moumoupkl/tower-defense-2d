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
    public InputActionReference buyTurret1; // Input action for purchasing turret 1
    public InputActionReference buyTurret2; // Input action for purchasing turret 2
    private InputActionReference move;
    public GameManager gameManager;
    public ObjectSpawner objectSpawner;

    [SerializeField]
    private Vector2 currentPosition = new Vector2(1.5f, 5.5f);

    private GameObject lastSelectedObject;

    void Start()
    {
        Camera mainCamera = Camera.main;
        gameManager = mainCamera.GetComponent<GameManager>();

        // Initialize the selection GameObject at the starting position
        if (selectionPrefab != null)
        {
            selection = Instantiate(selectionPrefab, currentPosition, Quaternion.identity);
        }
        UpdateSelection();

        // Subscribe to both turret purchase actions, and differentiate between them using parameters
        buyTurret1.action.performed += ctx => OnBuyActionPerformed(1); // Pass turret type 1
        buyTurret2.action.performed += ctx => OnBuyActionPerformed(2); // Pass turret type 2
    }

    void Update()
    {
        move = movep1;
        inputTimer -= Time.deltaTime;

        Vector2 movedirection = move.action.ReadValue<Vector2>();
        float horizontal = movedirection.x;
        float vertical = movedirection.y;

        if (Mathf.Abs(horizontal) > Mathf.Abs(vertical))
        {
            vertical = 0; // Prioritize horizontal movement
        }
        else if (Mathf.Abs(vertical) > Mathf.Abs(horizontal))
        {
            horizontal = 0; // Prioritize vertical movement
        }

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

    // Handle the turret purchase based on the turretType parameter
    private void OnBuyActionPerformed(int turretType)
    {
        if (lastSelectedObject != null)
        {
            var tileScript = lastSelectedObject.GetComponent<tiles>();
            if (tileScript != null && tileScript.hover && !tileScript.activeConstruction)
            {
                int weaponPrice = tileScript.weaponPrice;
                bool hasEnoughCoins = blueTeam ? gameManager.blueCoins >= weaponPrice : gameManager.redCoins >= weaponPrice;

                if (!gameManager.pause && hasEnoughCoins)
                {
                    tileScript.activeConstruction = true;
                    gameManager.AddCoins(-weaponPrice, blueTeam);

                    // Select the correct turret to spawn based on the turretType
                    GameObject turretToSpawn = turretType == 1 ? tileScript.turret1 : tileScript.turret2;
                    StartCoroutine(tileScript.SpawnObject(turretToSpawn)); // Spawn the chosen turret

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
            Vector2 newPosition = currentPosition + new Vector2(direction.x, direction.y);
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

    private void OnDestroy()
    {
        buyTurret1.action.performed -= ctx => OnBuyActionPerformed(1);
        buyTurret2.action.performed -= ctx => OnBuyActionPerformed(2);
    }
}
