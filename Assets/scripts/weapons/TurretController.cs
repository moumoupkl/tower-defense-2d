using UnityEngine;

public class TurretController : TowerControler
{
    public float range = 10f; // Detection range for enemies
    public GameObject rangeIndicator; // Reference to the range indicator (circle)
    
    public Transform targetEnemy;
    private SpriteRenderer rangeRenderer;
    public bool aimingAtEnnemy;
    public bool teamHover;

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
        base.Update();
        if (!gameManager.pause)
        {
            // Find the closest enemy within range
            FindClosestEnemy();
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
