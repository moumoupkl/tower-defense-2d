using UnityEngine;
using System.Collections;

public class tiles : MonoBehaviour
{
    public GameObject turret1;
    public GameObject turret2;
    public Animator animator;
    public int weaponPrice;            // The price of the weapon, still used for game balance but not for buying logic here
    public GameManager gameManager;    // Reference to the game manager for currency, still used for starting construction
    public bool hover;                 // Set to true when this tile is hovered over by the selector
    public bool activeConstruction;    // True when construction is active, prevents new construction until it's done
    public float constructionTime;     // Time it takes to construct the turret
    public GameObject particles;       // Visual effect for turret construction

    void Start()
    {
        hover = false;
        Camera mainCamera = Camera.main;
        gameManager = mainCamera.GetComponent<GameManager>();
    }

    void Update()
    {
        // Set the animator's "ishover" parameter based on the hover state
        if (hover && !activeConstruction)
        {
            animator.SetBool("ishover", true);
        }
        else
        {
            animator.SetBool("ishover", false);
        }
    }

    // Start the construction of the turret (triggered externally by DynamicGridSelector)
    public IEnumerator SpawnObject(GameObject turret)
    {
        // Reduce coins in GameManager (done externally in DynamicGridSelector)
        if (particles != null)
        {
            Debug.Log("Spawning particles at: " + transform.position);
            GameObject spawnedParticles = Instantiate(particles, transform.position, Quaternion.identity);
            Particle particleScript = spawnedParticles.GetComponent<Particle>();

            if (particleScript != null)
            {
                particleScript.SetLifetime(constructionTime);
            }
        }

        float elapsedTime = 0f;

        // Timer for construction time
        while (elapsedTime < constructionTime)
        {
            if (!gameManager.pause)
            {
                elapsedTime += Time.deltaTime;
            }
            yield return null;
        }

        // After construction time, spawn the turret and deactivate the tile
        Instantiate(turret, transform.position, Quaternion.identity);
        gameObject.SetActive(false);
    }
}
