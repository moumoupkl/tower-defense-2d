using UnityEngine;

public class TurretRotation : TurretController
{
    public Transform gun; // The part of the turret that rotates, e.g., a barrel or gun

    protected override void Update()
    {
        base.Update();

        // Only aim if there is a target
        if (targetEnemy != null)
        {
            AimAtTarget();
        }
    }

    private void AimAtTarget()
    {
        // Determine the direction to the target enemy
        Vector2 direction = (targetEnemy.position - gun.position).normalized;

        // Calculate the angle between the gun's forward vector and the direction to the target
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        // Instantly snap to the target enemy's direction
        gun.rotation = Quaternion.Euler(0, 0, angle + 180);
    }
}
