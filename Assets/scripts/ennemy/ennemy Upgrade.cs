using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnnemyUpgrade : MonoBehaviour
{
    public bool blueTeam;

    // Class to hold ennemy data
    [System.Serializable]
    public class EnnemyData
    {
        public GameObject ennemy;
        public int upgradeCost;
        public int upgradeCounter;
        public int upgradeLevel;
    }

    // List of all ennemies with their data
    public List<EnnemyData> ennemiesData = new List<EnnemyData>();

    // Max upgrade levels
    public int maxUpgradeLevel = 3;

    // Start is called before the first frame update
    void Start()
    {
        // Initialize upgradeCounter and upgradeLevel to 0 for each ennemy
        foreach (var ennemyData in ennemiesData)
        {
            ennemyData.upgradeCounter = 0;
            ennemyData.upgradeLevel = 0;
        }
    }

    // Increment the upgrade counter of a given ennemy
    public void incrementUpgradeCounter(GameObject ennemy, bool spawnIsBlueTeam)
    {
        // Only do this if right team
        if (blueTeam == spawnIsBlueTeam)
        {
            var ennemyData = ennemiesData.Find(e => e.ennemy == ennemy);
            if (ennemyData != null)
            {
                ennemyData.upgradeCounter++;

                // If ennemy has been bought enough times, upgrade it
                if (ennemyData.upgradeCounter >= ennemyData.upgradeCost)
                {
                    ennemyData.upgradeCounter = 0;
                    ennemyData.upgradeLevel++;

                    // If ennemy has reached max upgrade level, set it to max upgrade level
                    if (ennemyData.upgradeLevel > maxUpgradeLevel)
                    {
                        ennemyData.upgradeLevel = maxUpgradeLevel;
                    }
                    else
                    {
                        if (blueTeam)
                        {
                            Debug.Log("blueteam's " + ennemy.name + " has been upgraded to level " + ennemyData.upgradeLevel);
                        }
                        else
                        {
                            Debug.Log("redteam's " + ennemy.name + " has been upgraded to level " + ennemyData.upgradeLevel);
                        }
                    }
                }
            }
        }
    }
}