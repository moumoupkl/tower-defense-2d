using UnityEngine;
using UnityEngine.InputSystem;

public class ButtonHandler : MonoBehaviour
{
    public bool blueTeam;
    public GameObject groundTroup;
    public GameObject flyingTroup;

    private ObjectSpawner objectSpawner;

    // Input actions for troop spawning
    public InputActionReference spawnGroundTroup;     // Reference for player ground troop spawn
    public InputActionReference spawnFlyingTroup;     // Reference for player flying troop spawn

    void Start()
    {
        objectSpawner = GetComponent<ObjectSpawner>();

        // Subscribe to the input actions
        spawnGroundTroup.action.performed += ctx => objectSpawner.SpawnTroops(groundTroup, blueTeam);
        spawnFlyingTroup.action.performed += ctx => objectSpawner.SpawnTroops(flyingTroup, blueTeam);
    }

    void OnEnable()
    {
        // Enable input actions
        Debug.Log("OnEnable button handler");
        spawnGroundTroup.action.Enable();
        spawnFlyingTroup.action.Enable();
    }

    void OnDisable()
    {
        // Disable input actions
        Debug.Log("OnDisable button handler");
        spawnGroundTroup.action.Disable();
        spawnFlyingTroup.action.Disable();
    }

    void OnDestroy()
    {
        // Unsubscribe when the object is destroyed
        spawnGroundTroup.action.performed -= ctx => objectSpawner.SpawnTroops(groundTroup, blueTeam);
        spawnFlyingTroup.action.performed -= ctx => objectSpawner.SpawnTroops(flyingTroup, blueTeam);
    }
}
