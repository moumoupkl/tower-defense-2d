using UnityEngine;

public class TurretController : TowerControler
{
    public float range = 10f; // Detection range for enemies
    public GameObject rangeIndicator; // Reference to the range indicator (circle)
    
    [HideInInspector]
    public Transform targetEnemy;
    private SpriteRenderer rangeRenderer;

    [HideInInspector]
    public bool aimingAtEnnemy;
    [HideInInspector]
    public bool teamHover;
    public bool aimingAtClosestEnnemy;

    protected override void Start()
    {
        base.Start();
        aimingAtEnnemy=true;

        // Set up the range indicator
        if (rangeIndicator != null)
        {
            rangeRenderer = rangeIndicator.GetComponent<SpriteRenderer>();
            rangeIndicator.transform.localScale = new Vector3(range * 2, range * 2, 1); // Adjust to match range
            rangeRenderer.enabled = false; // Hide range indicator by default
        }

    }

    protected override void Update()
    {
        //set aiming at ennemy to false if there are no ennemies
        if (targetEnemy == null)
        {
            aimingAtEnnemy = false;
        }

        base.Update();
        if (!gameManager.pause)
        {
            // Find the closest enemy within range
            if (aimingAtClosestEnnemy)
            {
                FindClosestEnemy();
            }
            else
            {
                FindFurthestEnemyOnTrack();
            }
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
            enemyStats enemyStats = enemy.GetComponent<enemyStats>();
            if (enemyStats.blueTeam != objectStats.blueTeam)
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

    protected void FindFurthestEnemyOnTrack()
    {
        // Find all enemies in the scene
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        float highestProgress = -Mathf.Infinity;
        Transform furthestEnemy = null;

        // Iterate through all enemies to find the one furthest on the track within range
        foreach (GameObject enemy in enemies)
        {
            enemyStats enemyStats = enemy.GetComponent<enemyStats>();
            TroupMovement TroupMovement = enemy.GetComponent<TroupMovement>();
            if (enemyStats.blueTeam != objectStats.blueTeam)
            {
                float distanceToEnemy = Vector2.Distance(transform.position, enemy.transform.position);
                if (distanceToEnemy <= range && TroupMovement.progress > highestProgress)
                {
                    highestProgress = TroupMovement.progress;
                    furthestEnemy = enemy.transform;
                }
            }
        }

        // Set the furthest enemy as the target
        targetEnemy = furthestEnemy;
    }

    protected void selected()
    {
        if (objectStats.hover)
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
