using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    public InputActionReference move1;
    public InputActionReference move2;
    public GameManager gameManager;

    void Start()
    {
        //get gamemanger from main camera
        gameManager = Camera.main.GetComponent<GameManager>();
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
}