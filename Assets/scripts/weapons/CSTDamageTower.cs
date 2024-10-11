using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CSTDamageTower : TurretController
{

    public ShootBehavior shootBehavior; // Reference to the ShootBehavior scrip


    protected override void Start()
    {
        base.Start();
        
        
        if (shootBehavior == null)
        {
            shootBehavior = GetComponent<ShootBehavior>();
        }


    }

    protected override void Update()
    {   
        base.Update();

        if (!gameManager.pause)
        {


        Debug.Log(targetEnemy);
            // Find the closest enemy within range

            if (targetEnemy != null)
            {
                Debug.Log("target");
                animator.SetBool("fight", true);

                // Check if the enemy is within range and shoot if the cooldown has passed
                float distanceToEnemy = Vector2.Distance(transform.position, targetEnemy.position);
                if (distanceToEnemy <= range)
                {
                    if (shootBehavior != null)
                    {
                        Debug.Log("shoot csnt");
                        shootBehavior.Shoot();
                    
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
