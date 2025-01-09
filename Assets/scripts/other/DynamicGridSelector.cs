using System.Collections;
using UnityEngine;

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
    public GameManager gameManager;
    public Transform startingPoint;
    public selector selector; // Reference to the Selector script

    [SerializeField]
    private Vector2 currentPosition = new Vector2(1.5f, 5.5f);

    private GameObject lastSelectedObject;
    private TroopsAndTowers troopsAndTowers;

    void Start()
    {
        troopsAndTowers = GetComponent<TroopsAndTowers>();
        InitializeSelection();
    }

    void Update()
    {
        CheckHoverState();
    }

    // Initializes the selection object by setting its position and hover state.
    private void InitializeSelection()
    {
        inputTimer = 0;
        selection.transform.position = startingPoint.position;
        currentPosition = startingPoint.position;
        lastSelectedObject = startingPoint.gameObject;
        SetHoverState(lastSelectedObject, true);
    }

    // Handles the movement of the selection object based on the input direction.
    // Input: Vector2 moveDirection - the direction in which to move the selection.
    public void HandleMovement(Vector2 moveDirection)
    {
        if (!this.enabled) return; // Check if the script is active

        inputTimer -= Time.deltaTime;

        Vector2 direction = GetMovementDirection(moveDirection);

        if (inputTimer <= 0 && direction != Vector2.zero)
        {
            MoveSelection(direction);
            inputTimer = inputDelay;
        }

        if (direction == Vector2.zero) inputTimer = 0;
    }

    // Determines the movement direction based on the input direction.
    // Input: Vector2 moveDirection - the input direction.
    // Output: Vector2 - the normalized movement direction.
    private Vector2 GetMovementDirection(Vector2 moveDirection)
    {
        float horizontal = moveDirection.x;
        float vertical = moveDirection.y;

        if (Mathf.Abs(horizontal) >= Mathf.Abs(vertical)) vertical = 0;
        else horizontal = 0;

        return new Vector2(horizontal, vertical).normalized;
    }

    // Returns the last selected object.
    // Output: GameObject - the last selected object.
    public GameObject GetLastSelectedObject()
    {
        return lastSelectedObject;
    }

    // Moves the selection object in the specified direction.
    // Input: Vector2 direction - the direction in which to move the selection.
    private void MoveSelection(Vector2 direction)
    {
        if (gameManager.pause || selection == null) return;

        Vector2 newPosition = currentPosition + direction;
        newPosition.x = Mathf.Clamp(newPosition.x, minX, maxX);
        newPosition.y = Mathf.Clamp(newPosition.y, minY, maxY);

        selection.transform.position = newPosition;
        currentPosition = newPosition;

        CheckAndCorrectSelectorPosition();
    }

    // Checks and corrects the position of the selection object if it goes out of bounds.
    private void CheckAndCorrectSelectorPosition()
    {
        if (selection.transform.position.x < minX || selection.transform.position.x > maxX ||
            selection.transform.position.y < minY || selection.transform.position.y > maxY)
        {
            selection.transform.position = currentPosition;
        }
    }

    // Gets the object at the specified position.
    // Input: Vector2 position - the position to check for an object.
    // Output: GameObject - the object at the position, or null if no object is found.
    private GameObject GetObjectAtPosition(Vector2 position)
    {
        RaycastHit2D hit = Physics2D.Raycast(position, Vector2.zero);
        if (hit.collider != null && (hit.collider.CompareTag(tileTag) || hit.collider.CompareTag(weaponTag)))
        {
            return hit.collider.gameObject;
        }
        return null;
    }

    // Sets the hover state of the specified object.
    // Input: GameObject obj - the object to set the hover state for.
    //        bool state - the hover state to set (true for hover, false for not hover).
    private void SetHoverState(GameObject obj, bool state)
    {
        if (obj == null) return;

        var component = obj.GetComponent<ObjectStats>();
        if (component != null && component.blueTeam == blueTeam)
        {
            if (state) component.setHoversTrue();
            else component.setHoversFalse();
        }

        if (obj.CompareTag(tileTag) && selector != null)
        {
            selector.hover = state;
        }
    }

    // Continuously checks and updates the hover state of the selection object.
    private void CheckHoverState()
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
}