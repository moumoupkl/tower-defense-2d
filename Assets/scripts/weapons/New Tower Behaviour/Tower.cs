using UnityEditor.Networking.PlayerConnection;
using UnityEngine;

public class Tower : MonoBehaviour
{
    public TowerData towerData; // ScriptableObject holding all customizable stats
    public TowerConeDetection coneDetection; // Reference to the cone detection script

    public float fireCooldown;
    public Transform firePoint;

    void Start()
    {
        // Initialize tower stats
        fireCooldown = 0f;
    }

    void Update()
    {
        HandleFiring();
    }

    private void HandleFiring()
    {
        if (coneDetection.targetInSight == true && fireCooldown <= 0f)
        {
            Fire();
            fireCooldown = 1f / towerData.attackSpeed; // Attack speed as attacks/second
        }
        fireCooldown -= Time.deltaTime;
    }

    private void Fire()
    {
        if (towerData.isAoe)
        {
            // Perform an AoE attack
            PerformAoEAttack();
        }
        else
        {
            // Regular single-target attack (e.g., shooting projectiles)
            ShootProjectile();
        }
    }

    private void PerformAoEAttack()
    {
        // Find all enemies within the AoE radius and deal damage to them and instantiate a projectile
        Collider2D[] enemiesInRange = Physics2D.OverlapCircleAll(transform.position, towerData.range / 2, coneDetection.targetLayer);
        Instantiate(towerData.projectilePrefab, firePoint.position, firePoint.rotation);
        //draw a circle in the scene view
        foreach (Collider2D enemy in enemiesInRange)
        {
            enemyStats enemyStats = enemy.GetComponent<enemyStats>();
            if (enemyStats != null && enemyStats.blueTeam != GetComponent<ObjectStats>().blueTeam)
            {
                enemyStats.TakeDamage(towerData.aoeDamage);
            }
        }
    }

    private void ShootProjectile()
    {
        // Instantiate a projectile and set its target
        GameObject projectile = Instantiate(towerData.projectilePrefab, firePoint.position, firePoint.rotation);
        Projectile projectileScript = projectile.GetComponent<Projectile>();
        // Set the projectile's target to the current target and put an offset depening on the accuracy
        projectileScript.CurrentTarget = coneDetection.currentTarget.transform;
        //get blueteam from objectstats
        projectileScript.blueTeam = GetComponent<ObjectStats>().blueTeam;
    }

}
