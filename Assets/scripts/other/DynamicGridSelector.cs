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
    private Tile lastSelectedTile;
    private const int WEAPON_PRICE = 5;

    void Start()
    {
        move = movep1;
        selection = Instantiate(selectionPrefab, currentPosition, Quaternion.identity);
        inputTimer = 0;
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

    private void OnBuyActionPerformed(int turretType)
    {
        if (lastSelectedObject != null)
        {
            if (lastSelectedTile == null)
            {
                lastSelectedTile = lastSelectedObject.GetComponent<Tile>();
            }

            if (lastSelectedTile != null && lastSelectedTile.hover && !lastSelectedTile.activeConstruction)
            {
                bool hasEnoughCoins = blueTeam ? gameManager.blueCoins >= WEAPON_PRICE : gameManager.redCoins >= WEAPON_PRICE;

                if (!gameManager.pause && hasEnoughCoins)
                {
                    lastSelectedTile.activeConstruction = true;
                    gameManager.AddCoins(-WEAPON_PRICE, blueTeam);

                    // Select the correct turret to spawn
                    GameObject turretToSpawn = turretType == 1 ? lastSelectedTile.turretPrefab1 : lastSelectedTile.turretPrefab2;
                    StartCoroutine(lastSelectedTile.SpawnObject(turretToSpawn));  // Start construction

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
            // Ensure the new position is within bounds
            newPosition.x = Mathf.Clamp(newPosition.x, minX, maxX);
            newPosition.y = Mathf.Clamp(newPosition.y, minY, maxY);

            // Update the selection position
            selection.transform.position = newPosition;
            currentPosition = newPosition;

            // Update the last selected object
            lastSelectedObject = GetTileAtPosition(newPosition);
            if (lastSelectedObject != null)
            {
                lastSelectedTile = lastSelectedObject.GetComponent<Tile>();
            }
        }
    }

    private GameObject GetTileAtPosition(Vector2 position)
    {
        RaycastHit2D hit = Physics2D.Raycast(position, Vector2.zero);
        if (hit.collider != null && hit.collider.CompareTag(tileTag))
        {
            return hit.collider.gameObject;
        }
        return null;
    }

    private void CheckAndCorrectSelectorPosition()
    {
        if (selection.transform.position.x < minX || selection.transform.position.x > maxX ||
            selection.transform.position.y < minY || selection.transform.position.y > maxY)
        {
            selection.transform.position = currentPosition;
        }
    }
}