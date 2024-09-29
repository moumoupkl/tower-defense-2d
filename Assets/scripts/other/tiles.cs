using UnityEngine;
using System.Collections;

public class Tile : MonoBehaviour
{
    public GameObject turretPrefab1;
    public GameObject turretPrefab2;
    private Animator animator;
    public bool hover;
    public bool activeConstruction;
    private float constructionTime;
    private GameObject particles;
    private bool blueTeam;

    void Start()
    {
        animator = GetComponent<Animator>();
        hover = false;
    }

    void Update()
    {
        // Update animation based on hover and construction state
        animator.SetBool("ishover", hover && !activeConstruction);
    }

    // Start turret construction
    public IEnumerator SpawnObject(GameObject turretPrefab)
    {
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
        if (turretInstance.TryGetComponent(out TurretController turretController))
        {
            turretController.blueTeam = blueTeam;
        }

        // Mark tile as inactive after construction
        activeConstruction = false;
        gameObject.SetActive(false);
    }
}