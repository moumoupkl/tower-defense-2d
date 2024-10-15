using UnityEngine;

public class MovementLogic : MonoBehaviour
{
    //public GameObject selection;
    public float maxX = 5.5f;
    public float minX = -4.5f;
    public float maxY = 10.5f;
    public float minY = 0.5f;
    public float inputDelay = 0.5f;
    private float inputTimer;
    private Vector2 currentPosition;

    void Start()
    {
        inputTimer = 0;
        currentPosition = transform.position;
    }

    void Update()
    {
        // No input handling here, only movement logic
    }

    public void HandleMovement(Vector2 moveDirection)
    {
        inputTimer -= Time.deltaTime;

        Vector2 direction = GetMovementDirection(moveDirection);

        if (inputTimer <= 0 && direction != Vector2.zero)
        {
            MoveSelection(direction);
            inputTimer = inputDelay;
        }

        if (direction == Vector2.zero) inputTimer = 0;
    }

    private Vector2 GetMovementDirection(Vector2 moveDirection)
    {
        float horizontal = moveDirection.x;
        float vertical = moveDirection.y;

        if (Mathf.Abs(horizontal) >= Mathf.Abs(vertical)) vertical = 0;
        else horizontal = 0;

        return new Vector2(horizontal, vertical).normalized;
    }

    private void MoveSelection(Vector2 direction)
    {

        Vector2 newPosition = currentPosition + direction;
        newPosition.x = Mathf.Clamp(newPosition.x, minX, maxX);
        newPosition.y = Mathf.Clamp(newPosition.y, minY, maxY);

        transform.position = newPosition;
        currentPosition = newPosition;
    }
}