using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnnemyUpgrade : MonoBehaviour
{
    public bool blueTeam;
    //list of all ennemies
    public List<GameObject> ennemies = new List<GameObject>();

    //list that represent how many time a unity needs to be bought before upgrading
    public List<int> upgradeCosts = new List<int>();

    //list that show how many time a unity has been bought
    public List<int> upgradeCounter = new List<int>();
    
    // list that show the current upgrade level of the ennemy
    public List<int> upgradeLevels = new List<int>();
    //max upgrade levels
    public int maxUpgradeLevel = 2;

    // Start is called before the first frame update
    void Start()
    {
        //set upgradecounter and upgradelevel to 0 and to same size as ennemies
        for (int i = 0; i < ennemies.Count; i++)
        {
            upgradeCounter.Add(0);
            upgradeLevels.Add(0);
        }
        
    }

    // increment the upgrade counter of a given ennemy
    public void incrementUpgradeCounter(GameObject ennemy)
    {
        int index = ennemies.IndexOf(ennemy);
        upgradeCounter[index]++;

        //if ennemy has been bought enough time upgrade it
        if (upgradeCounter[index] >= upgradeCosts[index])
        {
            upgradeCounter[index] = 0;
            upgradeLevels[index]++;

            //if ennemy has reached max upgrade level set it to max upgrade level
            if (upgradeLevels[index] >= maxUpgradeLevel)
            {
                upgradeLevels[index] = maxUpgradeLevel;
            }
            else
            {
                Debug.Log(ennemy.name + " has been upgraded to level " + upgradeLevels[index]);
            }
        }
    }



    
}
