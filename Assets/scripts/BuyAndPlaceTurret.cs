using System.Collections;
using UnityEngine;

public class BuyAndPlaceTurret : MonoBehaviour
{
    private GameManager gameManager;
    private TroopsAndTowers troopsAndTowers;

    private void Start()
    {
        gameManager = GetComponent<GameManager>();
        troopsAndTowers = GetComponent<TroopsAndTowers>();
    }

    public void OnBuyActionPerformed(int turretType, bool isBlueTeam, GameObject lastSelectedObject) // buy a turret
    {
        if (lastSelectedObject == null) return;

        var lastSelectedTile = lastSelectedObject.GetComponent<Tile>();
        var lastSelectedComponent = lastSelectedObject.GetComponent<ObjectStats>();

        if (lastSelectedTile != null && lastSelectedComponent != null && lastSelectedComponent.hover && !lastSelectedTile.activeConstruction)
        {
            if (CanAffordTurret(turretType, isBlueTeam))
            {
                lastSelectedTile.activeConstruction = true;
                int turretPrice = troopsAndTowers.towerPrefabs[turretType].GetComponent<Price>().price;
                gameManager.AddCoins(-turretPrice, isBlueTeam);

                if (turretType >= 0 && turretType < troopsAndTowers.towerPrefabs.Count)
                {
                    GameObject turretToSpawn = troopsAndTowers.towerPrefabs[turretType];

                    // Start the construction coroutine
                    StartCoroutine(StartConstruction(lastSelectedTile, turretToSpawn, isBlueTeam));

                    lastSelectedObject = null;
                }
                else
                {
                    Debug.Log("Invalid turret type.");
                    Debug.Log("Turret type: " + turretType);
                    Debug.Log("Tower Prefabs Count: " + troopsAndTowers.towerPrefabs.Count);
                }
            }
            else
            {
                Debug.Log("Not enough coins or game is paused.");
            }
        }
    }

    private IEnumerator StartConstruction(Tile tile, GameObject turretPrefab, bool isBlueTeam)
    {
        // Set construction time to the construction time of the prefab
        float constructionTime = turretPrefab.GetComponent<TowerControler>().constructionTime;

        // Set particle time to construction time of the prefab
        if (tile.particles != null)
        {
            GameObject spawnedParticles = Instantiate(tile.particles, tile.transform.position, Quaternion.identity);
            if (spawnedParticles.TryGetComponent(out Particle particleScript))
            {
                particleScript.SetLifetime(constructionTime);
            }
        }

        // Wait for construction to complete
        yield return new WaitForSeconds(constructionTime);

        // Instantiate the turret and assign team
        GameObject turretInstance = Instantiate(turretPrefab, tile.transform.position, Quaternion.identity);
        if (turretInstance.TryGetComponent(out ObjectStats turretStats))
        {
            turretStats.blueTeam = tile.objectStats.blueTeam;
        }

        // Mark tile as inactive after construction
        tile.activeConstruction = false;
        tile.gameObject.SetActive(false);
    }

    private bool CanAffordTurret(int turretType, bool isBlueTeam) // check if the player can afford the turret
    {
        int turretPrice = troopsAndTowers.towerPrefabs[turretType].GetComponent<Price>().price;
        return !gameManager.pause && (isBlueTeam ? gameManager.blueCoins >= turretPrice : gameManager.redCoins >= turretPrice);
    }
}