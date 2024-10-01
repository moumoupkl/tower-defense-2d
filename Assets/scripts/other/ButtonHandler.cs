using UnityEngine;
using UnityEngine.InputSystem;

public class ButtonHandler : MonoBehaviour
{
    public GameObject groundTroup;
    public GameObject flyingTroup;

    private ObjectSpawner objectSpawner;

    // Input actions for troop spawning
    public InputActionReference spawnGroundTroupP1;     // Reference for player ground troop spawn
    public InputActionReference spawnFlyingTroupP1;     // Reference for player flying troop spawn
    public InputActionReference spawnGroundTroupP2;   // Reference for opponent ground troop spawn
    public InputActionReference spawnFlyingTroupP2;   // Reference for opponent flying troop spawn

    void Start()
    {
        objectSpawner = GetComponent<ObjectSpawner>();

        // Subscribe to the input actions
        spawnGroundTroupP1.action.performed += ctx => objectSpawner.SpawnTroups(groundTroup, true);
        spawnFlyingTroupP1.action.performed += ctx => objectSpawner.SpawnTroups(flyingTroup, true);
        spawnGroundTroupP2.action.performed += ctx => objectSpawner.SpawnTroups(groundTroup, false);
        spawnFlyingTroupP2.action.performed += ctx => objectSpawner.SpawnTroups(flyingTroup, false);
    }

    void OnEnable()
    {
        // Enable input actions
        Debug.Log("OnEnable button handler");
        spawnGroundTroupP1.action.Enable();
        spawnFlyingTroupP1.action.Enable();
        spawnGroundTroupP2.action.Enable();
        spawnFlyingTroupP2.action.Enable();
    }

    void OnDisable()
    {
        // Disable input actions
        Debug.Log("OnDisable button handler");
        spawnGroundTroupP1.action.Disable();
        spawnFlyingTroupP1.action.Disable();
        spawnGroundTroupP2.action.Disable();
        spawnFlyingTroupP2.action.Disable();
    }

    void OnDestroy()
    {
        // Unsubscribe when the object is destroyed
        spawnGroundTroupP1.action.performed -= ctx => objectSpawner.SpawnTroups(groundTroup, true);
        spawnFlyingTroupP1.action.performed -= ctx => objectSpawner.SpawnTroups(flyingTroup, true);
        spawnGroundTroupP2.action.performed -= ctx => objectSpawner.SpawnTroups(groundTroup, false);
        spawnFlyingTroupP2.action.performed -= ctx => objectSpawner.SpawnTroups(flyingTroup, false);
    }
}
