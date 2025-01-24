using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class BuyAndPlaceTurret : MonoBehaviour
{
    private GameManager gameManager;
    private TroopsAndTowers troopsAndTowers;
    private GameObject towers;

    private void Start()
    {
        gameManager = GetComponent<GameManager>();
        troopsAndTowers = GetComponent<TroopsAndTowers>();
        towers = GameObject.Find("Towers");
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
                int turretPrice = GetTurretPrice(turretType);
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
        float constructionTime;
        var towerControler = turretPrefab.GetComponent<TowerControler>();
        if (towerControler != null)
        {
            constructionTime = towerControler.constructionTime;
        }
        else
        {
            var towerData = turretPrefab.GetComponent<Tower>().towerData;
            constructionTime = towerData.builTime;
        }

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
        //offset  the y by + 0.3f
        turretInstance.transform.position += new Vector3(0, 0.3f, 0);

        //put the turret as child of the Towers gameobject
        turretInstance.transform.parent = towers.transform;

        //and its children if they have a sprite renderer
        foreach (SpriteRenderer sr in turretInstance.GetComponentsInChildren<SpriteRenderer>())
        {
            sr.sortingOrder = Mathf.RoundToInt(-turretInstance.transform.position.y);
        }

        // Mark tile as inactive after construction
        tile.activeConstruction = false;
        tile.gameObject.SetActive(false);
    }

    private bool CanAffordTurret(int turretType, bool isBlueTeam) // check if the player can afford the turret
    {
        int turretPrice = GetTurretPrice(turretType);
        return !gameManager.pause && (isBlueTeam ? gameManager.blueCoins >= turretPrice : gameManager.redCoins >= turretPrice);
    }

    private int GetTurretPrice(int turretType)
    {
        var turretPrefab = troopsAndTowers.towerPrefabs[turretType];
        var priceComponent = turretPrefab.GetComponent<Price>();
        if (priceComponent != null)
        {
            return priceComponent.price;
        }
        else
        {
            var towerData = turretPrefab.GetComponent<Tower>().towerData;
            return towerData.buildCost;
        }
    }
}