using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class WaveHandler : MonoBehaviour
{
    public troopCard troopCard;
    public bool blueTeam;
    public Wavetimer waveTimer;
    public float MaxTimeBetweenTroops;
    public int waveNumber;
    public int MaxTroopCapacity;
    public int currentTroopCapacity;
    public float spawnInterval;
    public bool wavestarted;
    private int troopLength;
    public ObjectSpawner objectSpawner;
    public List<GameObject> troops;
    private GameManager gameManager;
    public troup_order troup_order;

    void Start()
    {
        Camera mainCamera = Camera.main;
        gameManager = mainCamera.GetComponent<GameManager>();
        troops = new List<GameObject>();
        currentTroopCapacity = 0;
        waveNumber = 0;
        wavestarted = false;
    }

    void Update()
    {
        if (waveTimer.spawnPhase && !wavestarted)
        {
            Debug.Log("wave started");
            StartCoroutine(spawnWave());
            wavestarted = true;
        }
        if (!waveTimer.spawnPhase)
        {
            wavestarted = false;
        }
    }

    public void AddTroop(GameObject troop)
    {
        enemyStats enemyStats = troop.GetComponent<enemyStats>();

        if (blueTeam)
        {
            if (gameManager.blueCoins < enemyStats.price)
            {
                Debug.Log("Not enough blue coins");
                return;
            }

            if (enemyStats.capacity + currentTroopCapacity > MaxTroopCapacity)
            {
                Debug.Log("Not enough capacity for blue team");
                return;
            }

            gameManager.bluecoinsfloat -= enemyStats.price;
            gameManager.blueCoinsPerSec += 0.05f;
            //call spawncard from troup_order
            //troup_order.spawn_card(troop);
        }
        else
        {
            if (gameManager.redCoins < enemyStats.price)
            {
                Debug.Log("Not enough red coins");
                return;
            }

            if (enemyStats.capacity + currentTroopCapacity > MaxTroopCapacity)
            {
                Debug.Log("Not enough capacity for red team");
                return;
            }

            gameManager.redcoinsfloat -= enemyStats.price;
            gameManager.redCoinsPerSec += 0.05f;
        }

        currentTroopCapacity += enemyStats.capacity;
        troops.Add(troop);
        troopCard.spawn_card(troop);
    }

    public void RemoveTroop(GameObject troop)
    {
        if (troops.Contains(troop))
        {
            troops.Remove(troop);
            enemyStats enemyStats = troop.GetComponent<enemyStats>();
            currentTroopCapacity -= enemyStats.capacity;
        }
        else
        {
            Debug.Log("Troop not found");
        }
    }

    public IEnumerator spawnWave()
    {
        Debug.Log("Spawning wave with " + troops.Count + " troops.");

        if (troops.Count == 0)
        {
            Debug.LogWarning("No troops to spawn.");
            yield break;
        }

        currentTroopCapacity = 0;
        spawnInterval = Mathf.Min(MaxTimeBetweenTroops, waveTimer.waveSpawnDuration / troops.Count);

        troopLength = troops.Count;
        for (int i = 0; i < troopLength; i++)
        {
            Debug.Log("Spawning troop " + (i + 1) + " of " + troopLength);
            objectSpawner.SpawnTroops(troops[0], blueTeam);
            troops.RemoveAt(0);
            yield return new WaitForSeconds(spawnInterval);
        }

        wavestarted = false;
    }
}