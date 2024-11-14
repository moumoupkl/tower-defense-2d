using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    [Header("Player1")]
    public InputActionReference move1;
    public InputActionReference buyTurret1P1;
    public InputActionReference buyTurret2P1;
    public InputActionReference buyTurret3P1;
    public InputActionReference buyTurret4P1;
    public MovementLogic movementLogicP1;
    public DynamicGridSelector dynamicGridSelectorP1;

    [Header("Player2")]
    public InputActionReference move2;
    public InputActionReference buyTurret1P2;
    public InputActionReference buyTurret2P2;
    public InputActionReference buyTurret3P2;
    public InputActionReference buyTurret4P2;
    public MovementLogic movementLogicP2;
    public DynamicGridSelector dynamicGridSelectorP2;

    [Header("buy and place turret script")]
    private GameManager gameManager;


    public BuyAndPlaceTurret buyAndPlaceTurret;



    public bool menu1open;
    public bool menu2open;

    void Start()
    {
        // Get GameManager from main camera
        gameManager = Camera.main.GetComponent<GameManager>();

        // Subscribe to input actions
        move1.action.performed += OnMove1;
        buyTurret1P1.action.performed += OnBuyTurret1P1;
        buyTurret2P1.action.performed += OnBuyTurret2P1;
        buyTurret3P1.action.performed += OnBuyTurret3P1;
        buyTurret4P1.action.performed += OnBuyTurret4P1;

        move2.action.performed += OnMove2;
        buyTurret1P2.action.performed += OnBuyTurret1P2;
        buyTurret2P2.action.performed += OnBuyTurret2P2;
        buyTurret3P2.action.performed += OnBuyTurret3P2;
        buyTurret4P2.action.performed += OnBuyTurret4P2;

    }

    void Update()
    {
        if (Keyboard.current.escapeKey.wasPressedThisFrame && !gameManager.gameisover)
        {
            gameManager.pause = !gameManager.pause;
        }

        // Enable move actions if disabled
        if (!move1.action.enabled || !move2.action.enabled)
        {
            move1.action.Enable();
            move2.action.Enable();
        }
    }

    void OnDestroy()
    {
        // Unsubscribe from input actions
        move1.action.performed -= OnMove1;
        buyTurret1P1.action.performed -= OnBuyTurret1P1;
        buyTurret2P1.action.performed -= OnBuyTurret2P1;
        buyTurret3P1.action.performed -= OnBuyTurret3P1;

        move2.action.performed -= OnMove2;
        buyTurret1P2.action.performed -= OnBuyTurret1P2;
        buyTurret2P2.action.performed -= OnBuyTurret2P2;
        buyTurret3P2.action.performed -= OnBuyTurret3P2;
    }

    private void OnMove1(InputAction.CallbackContext context)
    {
        if (movementLogicP1 == null)
        {
            Debug.LogError("movementLogicP1 is not assigned.");
            return;
        }

        Vector2 movement = context.ReadValue<Vector2>();
        movementLogicP1.HandleMovement(movement);
        dynamicGridSelectorP1.HandleMovement(movement);
    }

    private void OnBuyTurret1P1(InputAction.CallbackContext context)
    {
        GameObject lastSelectedObject = dynamicGridSelectorP1.GetLastSelectedObject();
        buyAndPlaceTurret.OnBuyActionPerformed(0, true, lastSelectedObject); // Assuming turret type 0 for blue team
    }

    private void OnBuyTurret2P1(InputAction.CallbackContext context)
    {
        GameObject lastSelectedObject = dynamicGridSelectorP1.GetLastSelectedObject();
        buyAndPlaceTurret.OnBuyActionPerformed(1, true, lastSelectedObject); // Assuming turret type 1 for blue team
    }

    private void OnBuyTurret3P1(InputAction.CallbackContext context)
    {
        GameObject lastSelectedObject = dynamicGridSelectorP1.GetLastSelectedObject();
        buyAndPlaceTurret.OnBuyActionPerformed(2, true, lastSelectedObject); // Assuming turret type 2 for blue team
    }

    private void OnBuyTurret4P1(InputAction.CallbackContext context)
    {
        GameObject lastSelectedObject = dynamicGridSelectorP1.GetLastSelectedObject();
        buyAndPlaceTurret.OnBuyActionPerformed(3, true, lastSelectedObject); // Assuming turret type 2 for blue team
    }

    private void OnMove2(InputAction.CallbackContext context)
    {
        if (movementLogicP2 == null)
        {
            Debug.LogError("movementLogicP2 is not assigned.");
            return;
        }

        Vector2 movement = context.ReadValue<Vector2>();
        movementLogicP2.HandleMovement(movement);
        dynamicGridSelectorP2.HandleMovement(movement);
    }

    private void OnBuyTurret1P2(InputAction.CallbackContext context)
    {
        GameObject lastSelectedObject = dynamicGridSelectorP2.GetLastSelectedObject();
        buyAndPlaceTurret.OnBuyActionPerformed(0, false, lastSelectedObject); // Assuming turret type 0 for red team
    }

    private void OnBuyTurret2P2(InputAction.CallbackContext context)
    {
        GameObject lastSelectedObject = dynamicGridSelectorP2.GetLastSelectedObject();
        buyAndPlaceTurret.OnBuyActionPerformed(1, false, lastSelectedObject); // Assuming turret type 1 for red team
    }

    private void OnBuyTurret3P2(InputAction.CallbackContext context)
    {
        GameObject lastSelectedObject = dynamicGridSelectorP2.GetLastSelectedObject();
        buyAndPlaceTurret.OnBuyActionPerformed(2, false, lastSelectedObject); // Assuming turret type 2 for red team
    }

    private void OnBuyTurret4P2(InputAction.CallbackContext context)
    {
        GameObject lastSelectedObject = dynamicGridSelectorP2.GetLastSelectedObject();
        buyAndPlaceTurret.OnBuyActionPerformed(3, false, lastSelectedObject); // Assuming turret type 2 for red team
    }
}