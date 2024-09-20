using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreezerShootBehaviour : ShootBehavior
{
    public TriangRange triangRange;
    public float slowDuration;
    public float slowStrength;

    public override void Shoot()
    {
        foreach (var collider in triangRange.colliders)
        {
            // Get the GameObject that the collider is attached to
            GameObject colliderObject = collider.gameObject;

            // Try to get the script you want to affect (replace 'YourScript' with the actual script name)
            TroupMovement troupMovement = colliderObject.GetComponent<TroupMovement>();

            if (troupMovement != null)
            {
                // Perform your action on the script
                troupMovement.Slow(slowDuration, slowStrength); // Replace with the method you want to call
            }
        }
    }
}
