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
    public selector selector;

    [SerializeField]
    private Vector2 currentPosition = new Vector2(1.5f, 5.5f);

    public GameObject lastSelectedObject;
    private TroopsAndTowers troopsAndTowers;

    /// <summary>
    /// Initializes the component.
    /// </summary>
    void Start()
    {
        troopsAndTowers = GetComponent<TroopsAndTowers>();
        InitializeSelection();
    }

    /// <summary>
    /// Updates the component every frame.
    /// </summary>
    void Update()
    {
        CheckHoverState();
    }

    /// <summary>
    /// Initializes the selection.
    /// </summary>
    private void InitializeSelection()
    {
        if (selection == null || startingPoint == null) return; // Null check

        inputTimer = 0;
        selection.transform.position = startingPoint.position;
        currentPosition = startingPoint.position;
        lastSelectedObject = startingPoint.gameObject;
        SetHoverState(lastSelectedObject, true);
    }

    /// <summary>
    /// Handles the movement of the selection.
    /// </summary>
    /// <param name="moveDirection">The direction to move the selection.</param>
    public void HandleMovement(Vector2 moveDirection)
    {
        if (!this.enabled) return;

        inputTimer -= Time.deltaTime;

        Vector2 direction = GetMovementDirection(moveDirection);

        if (inputTimer <= 0 && direction != Vector2.zero)
        {
            MoveSelection(direction);
            inputTimer = inputDelay;
        }

        if (direction == Vector2.zero) inputTimer = 0;
    }

    /// <summary>
    /// Gets the movement direction based on the input.
    /// </summary>
    /// <param name="moveDirection">The input direction.</param>
    /// <returns>The normalized movement direction.</returns>
    private Vector2 GetMovementDirection(Vector2 moveDirection)
    {
        float horizontal = moveDirection.x;
        float vertical = moveDirection.y;

        if (Mathf.Abs(horizontal) >= Mathf.Abs(vertical)) vertical = 0;
        else horizontal = 0;

        return new Vector2(horizontal, vertical).normalized;
    }

    /// <summary>
    /// Gets the last selected object.
    /// </summary>
    /// <returns>The last selected GameObject.</returns>
    public GameObject GetLastSelectedObject()
    {
        return lastSelectedObject;
    }

    /// <summary>
    /// Moves the selection in the specified direction.
    /// </summary>
    /// <param name="direction">The direction to move the selection.</param>
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

    /// <summary>
    /// Checks and corrects the selector position if it is out of bounds.
    /// </summary>
    private void CheckAndCorrectSelectorPosition()
    {
        if (selection.transform.position.x < minX || selection.transform.position.x > maxX ||
            selection.transform.position.y < minY || selection.transform.position.y > maxY)
        {
            selection.transform.position = currentPosition;
        }
    }

    /// <summary>
    /// Gets the object at the specified position.
    /// </summary>
    /// <param name="position">The position to check.</param>
    /// <returns>The GameObject at the position, or null if none found.</returns>
    private GameObject GetObjectAtPosition(Vector2 position)
    {
        float detectionRadius = 0.1f; // Small radius for detection
        Collider2D[] hits = Physics2D.OverlapCircleAll(position, detectionRadius);
        foreach (var hit in hits)
        {
            if (hit.CompareTag(tileTag) || hit.CompareTag(weaponTag))
            {
                return hit.gameObject;
            }
        }
        return null;
    }

    /// <summary>
    /// Sets the hover state of the specified object.
    /// </summary>
    /// <param name="obj">The object to set the hover state for.</param>
    /// <param name="state">The hover state to set.</param>
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

    /// <summary>
    /// Checks and updates the hover state of the selection.
    /// </summary>
    private void CheckHoverState()
    {
        Vector2 newPosition = currentPosition;
        GameObject newSelectedObject = GetObjectAtPosition(newPosition);

        if (newSelectedObject != lastSelectedObject)
        {
            SetHoverState(lastSelectedObject, false);
            lastSelectedObject = newSelectedObject;
            SetHoverState(lastSelectedObject, true);
        }
    }
}