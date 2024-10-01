using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.InputSystem;

public class DynamicGridSelector : MonoBehaviour
{
    public bool blueTeam;
    public string tileTag;
    public string weaponTag;
    public GameObject selectionPrefab;
    public GameObject selection;
    public float maxX = 10f;
    public float minX = 0f;
    public float maxY = 10f;
    public float minY = 0f;
    public float inputDelay = 0.5f;
    private float inputTimer;
    public InputActionReference movep1;
    public InputActionReference buyTurret1;
    public InputActionReference buyTurret2;
    private InputActionReference move;
    public GameManager gameManager;
    public Transform startingPoint;

    [SerializeField]
    private Vector2 currentPosition = new Vector2(1.5f, 5.5f);

    private GameObject lastSelectedObject;
    private const int WEAPON_PRICE = 5;

    void Start()
    {
        InitializeSelection();
    }

    private void OnEnable()//subscribe to the input actions
    {
        buyTurret1.action.performed += context => OnBuyActionPerformed(1);
        buyTurret2.action.performed += context => OnBuyActionPerformed(2);
    }

    private void OnDisable()//unsubscribe from the input actions
    {
        buyTurret1.action.performed -= context => OnBuyActionPerformed(1);
        buyTurret2.action.performed -= context => OnBuyActionPerformed(2);
    }

    void Update()
    {
        HandleMovement();
        CheckAndCorrectSelectorPosition();
    }

    private void InitializeSelection()//initialize the selection object
    {
        move = movep1;
        selection = Instantiate(selectionPrefab, currentPosition, Quaternion.identity);
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

    private void OnBuyActionPerformed(int turretType)//buy a turret
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
                GameObject turretToSpawn = turretType == 1 ? lastSelectedTile.turretPrefab1 : lastSelectedTile.turretPrefab2;
                StartCoroutine(lastSelectedTile.SpawnObject(turretToSpawn));
                lastSelectedObject = null;
            }
            else
            {
                Debug.Log("Not enough coins or game is paused.");
            }
        }
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

        SetHoverState(lastSelectedObject, false);
        lastSelectedObject = GetObjectAtPosition(newPosition);
        SetHoverState(lastSelectedObject, true);
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