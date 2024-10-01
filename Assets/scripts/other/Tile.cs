using UnityEngine;
using System.Collections;

public class Tile : MonoBehaviour
{
    public GameObject turretPrefab1;
    public GameObject turretPrefab2;
    private Animator animator;
    public bool activeConstruction;
    private float constructionTime;
    public GameObject particles;
    private ObjectStats objectStats;

    void Start()
    {
        //get isseelected component
        objectStats = GetComponent<ObjectStats>();
        animator = GetComponent<Animator>();
        objectStats.hover = false;
    }

    void Update()
    {
        // Update animation based on hover and construction state
        animator.SetBool("ishover", objectStats.hover && !activeConstruction);
    }

    // Start turret construction
    public IEnumerator SpawnObject(GameObject turretPrefab)
    {
        //set constructiontime to the construction time of the prefab
        constructionTime = turretPrefab.GetComponent<TurretController>().constructionTime;
        
        //set particle time to construction time of the prefab
        if (particles != null)
        {
            GameObject spawnedParticles = Instantiate(particles, transform.position, Quaternion.identity);
            if (spawnedParticles.TryGetComponent(out Particle particleScript))
            {
                particleScript.SetLifetime(constructionTime);
            }
        }

        // Wait for construction to complete
        yield return new WaitForSeconds(constructionTime);

        // Instantiate the turret and assign team
        GameObject turretInstance = Instantiate(turretPrefab, transform.position, Quaternion.identity);
        if (turretInstance.TryGetComponent(out ObjectStats turretStats))
        {
            turretStats.blueTeam = objectStats.blueTeam;
        }

        // Mark tile as inactive after construction
        activeConstruction = false;
        gameObject.SetActive(false);
    }

}