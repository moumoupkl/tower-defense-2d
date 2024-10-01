using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{
    public GameManager gameManager;
    public Transform blueSpawnPoint;
    public Transform redSpawnPoint;
    private Transform spawnPoint;
    public EnnemyUpgrade blueEnnemyUpgrade;
    public EnnemyUpgrade redEnnemyUpgrade;
    

    void Start()
    {
        Camera mainCamera = Camera.main;
        gameManager = mainCamera.GetComponent<GameManager>();
    }

    public void SpawnTroops(GameObject objectToSpawn, bool blueTeam)
    {
        if (blueTeam)
        {
            // Check if the player has enough coins to spawn the object
            if (gameManager.blueCoins < 2)
            {
                return;
            }
            gameManager.blueCoins -= 2;
            gameManager.blueCoinsPerSec += 0.5f;

            //increase upgrade counter
            blueEnnemyUpgrade.incrementUpgradeCounter(objectToSpawn, blueTeam);

        }
        else
        {
            if (gameManager.redCoins < 2)
            {
                return;
            }
            gameManager.redCoins -= 2;
            gameManager.redCoinsPerSec += 0.5f;

            //increase upgrade counter
            redEnnemyUpgrade.incrementUpgradeCounter(objectToSpawn, blueTeam);
            
        }

        spawnPoint = blueTeam ? blueSpawnPoint : redSpawnPoint;

        if (!gameManager.pause)
        {
            // Spawn the object at the correct spawn point
            GameObject troop = Instantiate(objectToSpawn, spawnPoint.position, Quaternion.identity);

            // Get the TroopMovement component from the spawned object
            TroupMovement troopMovement = troop.GetComponent<TroupMovement>();
            enemyStats enemyStats = troop.GetComponent<enemyStats>();

            // Set the blueTeam bool in the TroopMovement script
            if (troopMovement != null)
            {
                troopMovement.blueTeam = blueTeam;
                if (enemyStats != null)
                {
                    enemyStats.blueTeam = blueTeam;
                }
            }
            else
            {
                Debug.LogError("TroopMovement component not found on the spawned object.");
            }
        }
    }

}