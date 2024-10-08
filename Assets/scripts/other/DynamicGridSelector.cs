using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.InputSystem;

public class DynamicGridSelector : MonoBehaviour
{
    public bool blueTeam;
    public string tileTag;
    public string weaponTag;
    public GameObject selection;
    public float maxX = 5.5f;
    public float minX = -4.5f;
    public float maxY = 10.5f;
    public float minY = 0.5f;
    public float inputDelay = 0.5f;
    private float inputTimer;
    public InputActionReference movep1;
    public InputActionReference buyTurret1;
    public InputActionReference buyTurret2;
    private InputActionReference move;
    public GameManager gameManager;
    public Transform startingPoint;
    public selector selector; // Reference to the Selector script

    [SerializeField]
    private Vector2 currentPosition = new Vector2(1.5f, 5.5f);

    private GameObject lastSelectedObject;
    private const int WEAPON_PRICE = 5;
    private TroopsAndTowers troopsAndTowers;

    void Start()
    {
        troopsAndTowers = GetComponent<TroopsAndTowers>();
        InitializeSelection();
    }

    private void OnEnable()//subscribe to the input actions
    {
        buyTurret1.action.performed += context => OnBuyActionPerformed(0);
        buyTurret2.action.performed += context => OnBuyActionPerformed(1);
    }

    private void OnDisable()//unsubscribe from the input actions
    {
        buyTurret1.action.performed -= context => OnBuyActionPerformed(0);
        buyTurret2.action.performed -= context => OnBuyActionPerformed(1);
    }

    void Update()
    {
        HandleMovement();
        CheckAndCorrectSelectorPosition();
        CheckHoverState();
    }

    private void InitializeSelection()//initialize the selection object
    {
        move = movep1;
        inputTimer = 0;
        selection.transform.position = startingPoint.position;
        currentPosition = startingPoint.position;
        lastSelectedObject = startingPoint.gameObject;
        SetHoverState(lastSelectedObject, true);
    }

    private void HandleMovement()//handle the movement of the selection object
    {
        move = movep1;
        inputTimer -= Time.deltaTime;

        Vector2 moveDirection = move.action.ReadValue<Vector2>();
        Vector2 direction = GetMovementDirection(moveDirection);

        if (inputTimer <= 0 && direction != Vector2.zero)
        {
            MoveSelection(direction);
            inputTimer = inputDelay;
        }

        if (direction == Vector2.zero) inputTimer = 0;
    }

    private Vector2 GetMovementDirection(Vector2 moveDirection)//get the movement direction
    {
        float horizontal = moveDirection.x;
        float vertical = moveDirection.y;

        if (Mathf.Abs(horizontal) >= Mathf.Abs(vertical)) vertical = 0;
        else horizontal = 0;

        return new Vector2(horizontal, vertical).normalized;
    }

    private void OnBuyActionPerformed(int turretType) // buy a turret
    {
        if (lastSelectedObject == null) return;

        var lastSelectedTile = lastSelectedObject.GetComponent<Tile>();
        var lastSelectedComponent = lastSelectedObject.GetComponent<ObjectStats>();

        if (lastSelectedTile != null && lastSelectedComponent != null && lastSelectedComponent.hover && !lastSelectedTile.activeConstruction)
        {
            if (CanAffordTurret())
            {
                lastSelectedTile.activeConstruction = true;
                gameManager.AddCoins(-WEAPON_PRICE, blueTeam);

                if (turretType >= 0 && turretType < troopsAndTowers.towerPrefabs.Count)
                {
                    GameObject turretToSpawn = troopsAndTowers.towerPrefabs[turretType];
                    
                    // Start the construction coroutine
                    StartCoroutine(StartConstruction(lastSelectedTile, turretToSpawn));

                    lastSelectedObject = null;
                }
                else
                {
                    Debug.Log("Invalid turret type.");
                    Debug.Log("Turret type: " + turretType);
                    Debug.Log("Tower Prefabs Count: " + troopsAndTowers.towerPrefabs.Count);
                }
            }
            else
            {
                Debug.Log("Not enough coins or game is paused.");
            }
        }
    }

    // Start turret construction
    private IEnumerator StartConstruction(Tile tile, GameObject turretPrefab)
    {
        // Set construction time to the construction time of the prefab
        float constructionTime = turretPrefab.GetComponent<TowerControler>().constructionTime;

        // Set particle time to construction time of the prefab
        if (tile.particles != null)
        {
            GameObject spawnedParticles = Instantiate(tile.particles, tile.transform.position, Quaternion.identity);
            if (spawnedParticles.TryGetComponent(out Particle particleScript))
            {
                particleScript.SetLifetime(constructionTime);
            }
        }

        // Wait for construction to complete
        yield return new WaitForSeconds(constructionTime);

        // Instantiate the turret and assign team
        GameObject turretInstance = Instantiate(turretPrefab, tile.transform.position, Quaternion.identity);
        if (turretInstance.TryGetComponent(out ObjectStats turretStats))
        {
            turretStats.blueTeam = tile.objectStats.blueTeam;
        }

        // Mark tile as inactive after construction
        tile.activeConstruction = false;
        tile.gameObject.SetActive(false);
    }

    private bool CanAffordTurret()//check if the player can afford the turret
    {
        return !gameManager.pause && (blueTeam ? gameManager.blueCoins >= WEAPON_PRICE : gameManager.redCoins >= WEAPON_PRICE);
    }

    private void MoveSelection(Vector2 direction)//move the selection object
    {
        if (gameManager.pause || selection == null) return;

        Vector2 newPosition = currentPosition + direction;
        newPosition.x = Mathf.Clamp(newPosition.x, minX, maxX);
        newPosition.y = Mathf.Clamp(newPosition.y, minY, maxY);

        selection.transform.position = newPosition;
        currentPosition = newPosition;
    }

    private GameObject GetObjectAtPosition(Vector2 position)//get the object at the position
    {
        RaycastHit2D hit = Physics2D.Raycast(position, Vector2.zero);
        if (hit.collider != null && (hit.collider.CompareTag(tileTag) || hit.collider.CompareTag(weaponTag)))
        {
            return hit.collider.gameObject;
        }
        return null;
    }

    private void SetHoverState(GameObject obj, bool state)//set the hover state of the object
    {
        if (obj == null) return;

        var component = obj.GetComponent<ObjectStats>();
        if (component != null && component.blueTeam == blueTeam)
        {
            if (state) component.setHoversTrue();
            else component.setHoversFalse();
        }

        if (obj.CompareTag(tileTag))
        {   
            // Set hover state of the selector
            if (selector != null)
            {
                selector.hover = state;
            }
        }
    }

    private void CheckHoverState()//check and update the hover state continuously
    {
        Vector2 newPosition = currentPosition;
        GameObject newSelectedObject = GetObjectAtPosition(newPosition);

        if (newSelectedObject != lastSelectedObject)
        {
            SetHoverState(lastSelectedObject, false); // Unset hover state of the previous object
            lastSelectedObject = newSelectedObject;
            SetHoverState(lastSelectedObject, true); // Set hover state of the new object
        }
    }

    private void CheckAndCorrectSelectorPosition()//check and correct the position of the selection object
    {
        if (selection.transform.position.x < minX || selection.transform.position.x > maxX ||
            selection.transform.position.y < minY || selection.transform.position.y > maxY)
        {
            selection.transform.position = currentPosition;
        }
    }
}