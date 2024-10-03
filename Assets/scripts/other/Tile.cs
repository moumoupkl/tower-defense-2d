using UnityEngine;
using System.Collections;

public class Tile : MonoBehaviour
{
    private Animator animator;
    public bool activeConstruction;
    private float constructionTime;
    public GameObject particles;
    private ObjectStats objectStats;

    void Start()
    {
        // Get ObjectStats and Animator components
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
    public IEnumerator StartConstruction(GameObject turretPrefab)
    {
        // Set construction time to the construction time of the prefab
        constructionTime = turretPrefab.GetComponent<TowerControler>().constructionTime;

        // Set particle time to construction time of the prefab
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