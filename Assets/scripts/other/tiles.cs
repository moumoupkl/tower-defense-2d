using UnityEngine;
using System.Collections;
using UnityEngine.InputSystem;

public class tiles : MonoBehaviour
{
    public GameObject turret1;
    public GameObject turret2;
    public Animator animator;
    public int weaponPrice;
    public GameManager gameManager;
    public bool hover;
    public InputActionReference select;
    public InputActionReference move;
    public bool activeConstruction;
    public float constructionTime;
    
    // Reference to the particle system prefab
    public ParticleSystem constructionParticles;  // Assign this in the inspector
    
    void Start()
    {
        hover = false;
        Camera mainCamera = Camera.main;
        gameManager = mainCamera.GetComponent<GameManager>();
    }

    void Update()
    {
        if (hover && !activeConstruction)
        {
            animator.SetBool("ishover", true);

            if (Input.GetKeyDown("space")) // Right-click for turret1
            {
                if (!gameManager.pause && gameManager.currentCoins >= weaponPrice)
                {
                    activeConstruction = true;
                    StartCoroutine(SpawnObject(turret1));
                }
            }
            if (Input.GetKeyDown("left shift")) // Right-click for turret2
            {
                if (!gameManager.pause && gameManager.currentCoins >= weaponPrice)
                {
                    activeConstruction = true;
                    StartCoroutine(SpawnObject(turret2));
                }
            }
        }
        else
        {
            animator.SetBool("ishover", false);
        }
    }

    IEnumerator SpawnObject(GameObject turret)
    {
        gameManager.AddCoins(-weaponPrice);
        // Set particle system duration to match construction time
        if (constructionParticles != null)
        {
            var mainModule = constructionParticles.main;
            mainModule.duration = constructionTime;  // Set the particle system duration
            constructionParticles.Play();  // Start playing the particle system
        }

        float elapsedTime = 0f;

        // Continue waiting until the elapsed time equals the construction time
        while (elapsedTime < constructionTime)
        {
            if (!gameManager.pause)
            {
                elapsedTime += Time.deltaTime;
            }
            yield return null;  // Wait for the next frame
        }

        // Perform the rest of the code after the timer completes
        Instantiate(turret, gameObject.transform.position, Quaternion.identity);

        if (constructionParticles != null)
        {
            constructionParticles.Stop();  // Stop the particle system once construction is complete
        }

        gameObject.SetActive(false);  // Deactivate the tile after construction

        if (!move.action.enabled)
        {
            move.action.Enable();
            Debug.Log("Re-enabled move action after tile deactivation.");
        }
    }
}
