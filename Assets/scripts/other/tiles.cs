using UnityEngine;
using System.Collections;

public class tiles : MonoBehaviour
{
    public GameObject turret1;
    public GameObject turret2;
    public Animator animator;
    public float constructionTime;
    public GameObject particles;
    public bool activeConstruction;
    public bool blueTeam;
    public bool hover;

    void Start()
    {
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
            Particle particleScript = spawnedParticles.GetComponent<Particle>();
            if (particleScript != null)
            {
                particleScript.SetLifetime(constructionTime);
            }
        }

        // Wait for construction to complete
        yield return new WaitForSeconds(constructionTime);

        // Instantiate the turret and assign team
        GameObject turretInstance = Instantiate(turretPrefab, transform.position, Quaternion.identity);
        TurretController turretController = turretInstance.GetComponent<TurretController>();
        if (turretController != null)
        {
            turretController.blueTeam = blueTeam;
        }

        // Mark tile as inactive after construction
        activeConstruction = false;
        gameObject.SetActive(false);
    }
}
