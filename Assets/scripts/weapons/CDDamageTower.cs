using UnityEngine;

public class CDDamageDower : TurretController
{
    public ShootBehavior shootBehavior; // Reference to the ShootBehavior scrip
    private float fireTimer; // Timer to track cooldown
    public float fireCooldown = 1f; // Cooldown time between shots in seconds
    public bool readyToShoot;


    public override void Start()
    {

        if (shootBehavior == null)
        {
            shootBehavior = GetComponent<ShootBehavior>();
        }

        fireTimer = fireCooldown; // Initialize the timer
    }

    public override void Update()
    {   
        Debug.Log("CDDamageTower Update");
        if (!gameManager.pause)
        {
            // Update the cooldown timer
            fireTimer += Time.deltaTime;

            // Find the closest enemy within range

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


    }

}
