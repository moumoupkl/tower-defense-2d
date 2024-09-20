using UnityEngine;

public class TurretController : MonoBehaviour
{
    public ShootBehavior shootBehavior; // Reference to the ShootBehavior script
    public float range = 10f; // Detection range for enemies
    public GameObject rangeIndicator; // Reference to the range indicator (circle)
    
    protected Transform targetEnemy;
    private SpriteRenderer rangeRenderer;
    public GameManager gameManager;
    public Animator animator;
    public bool hover;

    private float fireTimer; // Timer to track cooldown
    public float fireCooldown = 1f; // Cooldown time between shots in seconds
    public bool readyToShoot;
    public bool blueTeam;

    protected virtual void Start()
    {
        Camera mainCamera = Camera.main;
        gameManager = mainCamera.GetComponent<GameManager>();

        // Set up the range indicator
        if (rangeIndicator != null)
        {
            rangeRenderer = rangeIndicator.GetComponent<SpriteRenderer>();
            rangeIndicator.transform.localScale = new Vector3(range * 2, range * 2, 1); // Adjust to match range
            rangeRenderer.enabled = false; // Hide range indicator by default
        }

        if (shootBehavior == null)
        {
            shootBehavior = GetComponent<ShootBehavior>();
        }

        fireTimer = fireCooldown; // Initialize the timer
    }

    protected virtual void Update()
    {
        if (!gameManager.pause)
        {
            // Update the cooldown timer
            fireTimer += Time.deltaTime;

            // Find the closest enemy within range
            FindClosestEnemy();

            if (targetEnemy != null && readyToShoot)
            {
                animator.speed = 1f / fireCooldown * 10;
                animator.SetBool("fight", true);

                // Check if the enemy is within range and shoot if the cooldown has passed
                float distanceToEnemy = Vector2.Distance(transform.position, targetEnemy.position);
                if (distanceToEnemy <= range && fireTimer >= fireCooldown)
                {
                    if (shootBehavior != null)
                    {
                        if (readyToShoot)
                        {
                            shootBehavior.Shoot();
                            fireTimer = 0f; // Reset the cooldown timer after shooting
                        }
                    }
                }
            }
            else 
            {
                animator.SetBool("fight", false);
            }
        }
        else
        {
            animator.speed = 0;
        }

        selected();
    }

    protected void FindClosestEnemy()
    {
        // Find all enemies in the scene
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        float closestDistance = Mathf.Infinity;
        Transform closestEnemy = null;

        // Iterate through all enemies to find the closest one within range
        foreach (GameObject enemy in enemies)
        {
            TroupMovement enemyStats = enemy.GetComponent<TroupMovement>();
            if (enemyStats.blueTeam != blueTeam)
            {
                float distanceToEnemy = Vector2.Distance(transform.position, enemy.transform.position);
                if (distanceToEnemy < closestDistance && distanceToEnemy <= range)
                {
                    closestDistance = distanceToEnemy;
                    closestEnemy = enemy.transform;
                }
            }
        }

        // Set the closest enemy as the target
        targetEnemy = closestEnemy;
    }

    protected void selected()
    {
        if (hover)
        {
            if (rangeRenderer != null)
            {
                rangeRenderer.enabled = true;
            }
        }
        else
        {
            if (rangeRenderer != null)
            {
                rangeRenderer.enabled = false;
            }
        }
    }
}
