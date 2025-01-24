using System.Collections.Generic;
using UnityEngine;

public class TowerConeDetection : MonoBehaviour
{
    public TowerData towerData;
    public ObjectStats objectStats;
    [Range(0, 360)]
    public float coneAngle = 60f; // The angle of the cone
    public float coneRadius = 5f; // The radius of the cone (will be used for the circle detection as well)
    public LayerMask targetLayer; // LayerMask for detecting enemies

    public float rotationSpeed = 10f; // The speed at which the tower rotates
    public string targetingPriority; // The priority of the target
    public List<Collider2D> targetsInRange = new List<Collider2D>(); // List of targets in the circle

    public GameObject currentTarget; // The current target
    public bool targetInSight; // True if a target is in the cone

    void Start()
    {
        coneAngle = towerData.aimAngle;
        coneRadius = towerData.range / 2;
        rotationSpeed = towerData.rotationSpeed;
        transform.localScale = new Vector3(2 * coneRadius, 2 * coneRadius, 1);

        // get the circle collider component
        CircleCollider2D circleCollider = GetComponent<CircleCollider2D>();
    }

    void Update()
    {
        targetingPriority = towerData.targetingPriority.ToString();

        //if there is a target in the circle
        if (targetsInRange.Count > 0)
        {
            currentTarget = GetTargetBasedOnPriority();
            targetInSight = CheckForTargetInSight();
            if (currentTarget != null)
            {
                RotateTowardsTarget();
            }
            else
            {
                targetInSight = false;
            }
        }
        else
        {
            targetInSight = false;
        }
    }

    void RotateTowardsTarget()
    {
        Vector3 direction = currentTarget.transform.position - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f;
        Quaternion targetRotation = Quaternion.Euler(new Vector3(0, 0, angle));
        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (((1 << other.gameObject.layer) & targetLayer) != 0)
        {
            enemyStats enemy = other.GetComponent<enemyStats>();
            if (enemy != null && enemy.blueTeam != objectStats.blueTeam)
            {
                targetsInRange.Add(other); // Add target to the list when it enters the circle
            }
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (((1 << other.gameObject.layer) & targetLayer) != 0)
        {
            targetsInRange.Remove(other); // Remove target from the list when it exits the circle
        }
    }

    bool CheckForTargetInSight()
    {
        if (currentTarget == null)
        {
            return false; // No current target
        }

        Vector2 directionToEnemy = currentTarget.transform.position - transform.position;
        float angleToEnemy = Vector2.Angle(transform.up, directionToEnemy);

        if (angleToEnemy <= coneAngle / 2)
        {
            return true; // The current target is within the cone
        }

        return false; // The current target is not in the cone
    }

    GameObject GetTargetBasedOnPriority()
    {
        switch (targetingPriority)
        {
            case "First":
                return targetsInRange[0].gameObject;
            case "Last":
                return targetsInRange[targetsInRange.Count - 1].gameObject;
            case "Closest":
                return GetClosestTarget();
            default:
                return null;
        }
    }

    GameObject GetClosestTarget()
    {
        Collider2D closestTarget = null;
        float closestDistance = float.MaxValue;

        foreach (var target in targetsInRange)
        {
            float distance = Vector2.Distance(transform.position, target.transform.position);
            if (distance < closestDistance)
            {
                closestDistance = distance;
                closestTarget = target;
            }
        }

        return closestTarget?.gameObject;
    }

    private void OnDrawGizmos()
    {
        // Visualize the detection cone and circle
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, coneRadius); // Circle visualization

        float halfAngle = coneAngle / 2;
        Vector3 direction1 = Quaternion.Euler(0, 0, halfAngle) * transform.up;
        Vector3 direction2 = Quaternion.Euler(0, 0, -halfAngle) * transform.up;

        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.position + direction1 * coneRadius);
        Gizmos.DrawLine(transform.position, transform.position + direction2 * coneRadius);
    }
}