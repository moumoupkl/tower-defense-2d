using UnityEngine;

[ExecuteInEditMode] // This ensures the gizmos are drawn even when not playing.
public class TowerDetection : MonoBehaviour
{
    [Range(0, 360)]
    public float coneAngle = 60f; // The angle of the cone
    public float coneRadius = 5f; // The radius of the cone
    public LayerMask targetLayer; // LayerMask for detecting enemies

    public bool inCircle; // Is a target in the circle?
    public bool inCone;   // Is a target in the cone?

    void Update()
    {
        CheckTarget();
    }

    void CheckTarget()
    {
        // Reset booleans
        inCircle = false;
        inCone = false;

        // Detect all objects within the circle
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, coneRadius, targetLayer);

        foreach (var hit in hits)
        {
            inCircle = true; // At least one target is in the circle

            // Check if it's in the cone
            Vector2 directionToTarget = hit.transform.position - transform.position;
            float angleToTarget = Vector2.Angle(transform.up, directionToTarget);

            if (angleToTarget <= coneAngle / 2)
            {
                inCone = true;
                break; // Stop checking once we find a valid target in the cone
            }
        }
    }

    private void OnDrawGizmos()
    {
        // Only draw gizmos when the game is not playing
        if (!Application.isPlaying)
        {
            // Visualize the detection circle
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(transform.position, coneRadius);

            // Visualize the cone (fill it with color)
            Gizmos.color = new Color(1f, 0f, 0f, 0.2f); // Light red for the filled cone area
            DrawConeArea();

            // Visualize the cone boundaries (lines)
            Gizmos.color = Color.red;
            float halfAngle = coneAngle / 2;
            Vector3 direction1 = Quaternion.Euler(0, 0, halfAngle) * transform.up;
            Vector3 direction2 = Quaternion.Euler(0, 0, -halfAngle) * transform.up;

            Gizmos.DrawLine(transform.position, transform.position + direction1 * coneRadius);
            Gizmos.DrawLine(transform.position, transform.position + direction2 * coneRadius);
        }
    }

    // Function to draw a filled cone area
    void DrawConeArea()
    {
        int segments = 50; // Number of segments to draw for smoothness
        float step = coneAngle / segments;
        Vector3 previousPoint = transform.position;

        // Iterate over the angle range to create the cone shape
        for (int i = 0; i <= segments; i++)
        {
            float angle = -coneAngle / 2 + step * i;
            Vector3 point = transform.position + (Quaternion.Euler(0, 0, angle) * transform.up) * coneRadius;
            Gizmos.DrawLine(previousPoint, point);
            previousPoint = point;
        }
    }
}
