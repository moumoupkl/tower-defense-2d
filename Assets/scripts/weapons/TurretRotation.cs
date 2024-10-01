using UnityEngine;

public class TurretRotation : MonoBehaviour
{
    public Transform gun; // The part of the turret that rotates, e.g., a barrel or gun
    public Transform targetEnemy; // The target enemy to aim at
    public float aimingTolerance = 5f; // Degrees tolerance to consider the turret "facing" the target
    public float rotationSpeed = 100f;
    private TurretController turretController;

    void Start()
    {
        // Get the TurretController component on the same GameObject
        turretController = GetComponent<TurretController>();
        if (turretController == null)
        {
            Debug.LogError("TurretController component is missing!");
        }
    }

    void Update()
    {
        //get the target enemy from the TurretController
        targetEnemy = turretController.targetEnemy;
        
        // Only aim if there is a target
        if (targetEnemy != null)
        {
            Debug.Log("Aiming at target");
            AimAtTarget();
        }
    }

    private void AimAtTarget()
    {
        // Determine the direction to the target enemy
        Vector2 direction = (targetEnemy.position - gun.position).normalized;

        // Calculate the angle between the gun's forward vector and the direction to the target
        float targetAngle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        // Get the current rotation of the gun
        Quaternion currentRotation = gun.rotation;

        // Calculate the target rotation
        Quaternion targetRotation = Quaternion.Euler(0, 0, targetAngle + 180);

        // Smoothly rotate towards the target rotation
        gun.rotation = Quaternion.RotateTowards(currentRotation, targetRotation, rotationSpeed * Time.deltaTime);

        // Check if the turret is facing the target approximately
        CheckIfReadyToShoot(targetAngle);
    }

    private void CheckIfReadyToShoot(float targetAngle)
    {
        // Get the current rotation angle of the gun
        float currentAngle = gun.eulerAngles.z;

        // Calculate the angle difference between the gun and the target
        float angleDifference = Mathf.Abs(Mathf.DeltaAngle(currentAngle, targetAngle + 180));

        // If the difference is within the tolerance, the turret is ready to shoot
        bool isReadyToShoot = angleDifference <= aimingTolerance;

        // Set the readyToShoot property of the TurretController
        if (turretController != null)
        {
            turretController.readyToShoot = isReadyToShoot;
        }
    }
}