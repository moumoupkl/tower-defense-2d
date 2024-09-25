using UnityEngine;
using System.Collections;

public class tiles : MonoBehaviour
{
    public GameObject turret1;
    public GameObject turret2;
    public Animator animator;
    public float constructionTime;     // Time it takes to construct the turret
    public GameObject particles;       // Visual effect for turret construction
    public bool activeConstruction;    // True when construction is active
    public bool blueTeam;              // For determining which team the turret belongs to
    public bool hover;                 // Set to true when this tile is hovered over by the selector

    void Start()
    {
        hover = false;
    }

    void Update()
    {
        // Update the animation based on hover and construction state
        animator.SetBool("ishover", hover && !activeConstruction);
    }

    // Start the construction of the turret (called externally by DynamicGridSelector)
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

        // Simulate construction time
        float elapsedTime = 0f;
        while (elapsedTime < constructionTime)
        {
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Instantiate the turret
        GameObject turretInstance = Instantiate(turretPrefab, transform.position, Quaternion.identity);
        TurretController turretController = turretInstance.GetComponent<TurretController>();
        if (turretController != null)
        {
            turretController.blueTeam = blueTeam;  // Assign the team
        }

        // Mark tile as inactive after construction
        activeConstruction = false;
        gameObject.SetActive(false);
    }
}
