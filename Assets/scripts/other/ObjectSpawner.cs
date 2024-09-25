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

    public void SpawnTroups(GameObject objectToSpawn, bool blueTeam)
    {
        if (blueTeam)
        {
            if (gameManager.blueCoins < 2)
            {
                return;
            }
            else
            {
                gameManager.bluecoinsfloat -= 2;
                gameManager.bluecoinspersecs += 0.5f;
            }
        }

        else
        {
            if (gameManager.redCoins < 2)
            {
                return;
            }
            else
            {
                gameManager.redcoinsfloat -= 2;
                gameManager.redcoinspersecs += 0.5f;
            }
        }
        
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
            enemyStats enemyStats = troup.GetComponent<enemyStats>();

            // Set the blueTeam bool in the TroupMovement script
            if (troupMovement != null)
            {
                troupMovement.blueTeam = blueTeam;
                enemyStats.blueTeam = blueTeam;
            }
            else
            {
                Debug.LogError("TroupMovement component not found on the spawned object.");
            }
        }
    }
}
