using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{
    public GameManager gameManager;
    public Transform BlueSpawnPoint;
    public Transform RedSpawnPoint;
    private Transform spawnPoint;

    void Start()
    {
        Camera mainCamera = Camera.main;
        gameManager = mainCamera.GetComponent<GameManager>();
    }

    public void SpawnObject(GameObject objectToSpawn, bool blueTeam)
    {
        if (blueTeam)
        {
            spawnPoint = BlueSpawnPoint;
        }
        else
        {
            spawnPoint = RedSpawnPoint;
        }

        if (!gameManager.pause)
        {
            // Spawn the object at the correct spawn point
            GameObject troup = Instantiate(objectToSpawn, spawnPoint.position, Quaternion.identity);

            // Get the TroupMovement component from the spawned object
            TroupMovement troupMovement = troup.GetComponent<TroupMovement>();

            // Set the blueTeam bool in the TroupMovement script
            if (troupMovement != null)
            {
                troupMovement.blueTeam = blueTeam;
            }
            else
            {
                Debug.LogError("TroupMovement component not found on the spawned object.");
            }
        }
    }
}
