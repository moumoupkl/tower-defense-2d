using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class WaveHandler : MonoBehaviour
{
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
        if (blueTeam)
        {
            if (gameManager.blueCoins < 2)
            {
                return;
            }

            if (troop.GetComponent<enemyStats>().capacity + currentTroopCapacity > MaxTroopCapacity)
            {
                return;
            }

            gameManager.bluecoinsfloat -= troop.GetComponent<enemyStats>().price;
            gameManager.blueCoinsPerSec += 0.05f;

            currentTroopCapacity += troop.GetComponent<enemyStats>().capacity;
        }
        else
        {
            if (gameManager.redCoins < 2)
            {
                return;
            }
            gameManager.redcoinsfloat -= 2;
            gameManager.redCoinsPerSec += 0.05f;
        }

        if (currentTroopCapacity + troop.GetComponent<enemyStats>().capacity <= MaxTroopCapacity)
        {
            troops.Add(troop);
            enemyStats enemyStats = troop.GetComponent<enemyStats>();
            currentTroopCapacity += enemyStats.capacity;
        }
        else
        {
            Debug.Log("Not enough capacity");
        }
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
            objectSpawner.SpawnTroops(troops[i], blueTeam);
            yield return new WaitForSeconds(spawnInterval);
        }

        troops = new List<GameObject>();
        wavestarted = false;
    }
}