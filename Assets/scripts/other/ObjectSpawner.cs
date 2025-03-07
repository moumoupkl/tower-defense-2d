using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{
    public GameManager gameManager;
    public Transform blueSpawnPoint;
    public Transform redSpawnPoint;
    public GameObject spawnedTroops;
    public GameObject redspawnedTroops;
    private Transform spawnPoint;
    public EnnemyUpgrade blueEnnemyUpgrade;
    public EnnemyUpgrade redEnnemyUpgrade;
    //blue list of ennemies and their levels

    void Start()
    {
        Camera mainCamera = Camera.main;
        gameManager = mainCamera.GetComponent<GameManager>();
    }

    public void SpawnTroops(GameObject objectToSpawn, bool blueTeam)
    {
        if (blueTeam)
        {
            //increase upgrade counter
            blueEnnemyUpgrade.incrementUpgradeCounter(objectToSpawn, blueTeam);

        }
        else
        {
            redEnnemyUpgrade.incrementUpgradeCounter(objectToSpawn, blueTeam);
        }

        spawnPoint = blueTeam ? blueSpawnPoint : redSpawnPoint;

        if (!gameManager.pause)
        {
            // Spawn the object at the correct spawn point
            GameObject troop = Instantiate(objectToSpawn, spawnPoint.position, Quaternion.identity);
            // if blueteam set as child of spawnedtroops
            if (blueTeam)
            {
                troop.transform.parent = spawnedTroops.transform;
            }
            else
            {
                troop.transform.parent = redspawnedTroops.transform;
            }

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