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
        CheckAndCorrectSelectorPosition();
        CheckHoverState();
    }

    private void InitializeSelection()//initialize the selection object
    {
        inputTimer = 0;
        selection.transform.position = startingPoint.position;
        currentPosition = startingPoint.position;
        lastSelectedObject = startingPoint.gameObject;
        SetHoverState(lastSelectedObject, true);
    }

    public void HandleMovement(Vector2 moveDirection)//handle the movement of the selection object
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

    private Vector2 GetMovementDirection(Vector2 moveDirection)//get the movement direction
    {
        float horizontal = moveDirection.x;
        float vertical = moveDirection.y;

        if (Mathf.Abs(horizontal) >= Mathf.Abs(vertical)) vertical = 0;
        else horizontal = 0;

        return new Vector2(horizontal, vertical).normalized;
    }

    public GameObject GetLastSelectedObject() // get the last selected object
    {
        return lastSelectedObject;
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