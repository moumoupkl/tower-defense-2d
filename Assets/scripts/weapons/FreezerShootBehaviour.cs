using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreezerShootBehaviour : ShootBehavior
{
    public TriangRange triangRange;
    public float slowDuration;
    public float slowStrength;
    private TurretController TurretController;
    public Transform target;

    public override void Shoot()
    {
        TurretController = GetComponent<TurretController>();
        Debug.Log("Freeze");
        target = TurretController.targetEnemy;
        //rotate the triangle to face the target
        Vector3 dir = target.position - transform.position;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }
}
