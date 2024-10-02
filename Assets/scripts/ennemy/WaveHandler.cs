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
    private bool wavestarted;
    public ObjectSpawner objectSpawner;
    //lisst of troops that were bought
    public List<GameObject> troops;
    // Start is called before the first frame update
    void Start()
    {

        
    }

    // Update is called once per frame
    void Update()
    {
        //if sapwnphase true than spawnwave once
        if (waveTimer.spawnPhase && !wavestarted)
        {
            spawnWave();
            wavestarted = true;
        }
        
    }

    //add troops to the list and their capacity to the current capacity
    public void AddTroop(GameObject troop)
    {
        if (currentTroopCapacity + troop.GetComponent<enemyStats>().capacity <= MaxTroopCapacity)
        {
            troops.Add(troop);
            //get the enemyStats component from the troop
            enemyStats enemyStats = troop.GetComponent<enemyStats>();
            currentTroopCapacity += enemyStats.capacity; 
        }
        else
        {
            Debug.Log("Not enough capacity");
        }
        
    }

    //remove troops from the list and their capacity from the current capacity
    public void RemoveTroop(GameObject troop)
    {
        if (troops.Contains(troop))
        {
            troops.Remove(troop);
            //get the enemyStats component from the troop
            enemyStats enemyStats = troop.GetComponent<enemyStats>();
            currentTroopCapacity -= enemyStats.capacity;
        }
        else
        {
            Debug.Log("Troop not found");
        }
    }

    public void spawnWave()
    {
        //set spawn ineterval to minimum between the max time between troops and waveSpawnDuration/amount of troops in list
        spawnInterval = Mathf.Min(MaxTimeBetweenTroops, waveTimer.waveSpawnDuration / troops.Count);
        //spawn all troops in the list
        foreach (GameObject troop in troops)
        {
            Debug.Log("Spawning troop");
            objectSpawner.SpawnTroops(troop, blueTeam);
            //wait for the spawn interval
            StartCoroutine(WaitForSpawnInterval());
        }
        
        //remove all troops from the list
        troops.Clear();
    }

    //wait for the spawn interval
    private IEnumerator WaitForSpawnInterval()
    {
        yield return new WaitForSeconds(spawnInterval);
    }
}
