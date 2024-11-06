using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserShootBehaviour : MonoBehaviour
{
    private TurretController turretController;
    public Transform target;
    public float laserDamage = 10f;
    public LineRenderer laserLine;

    // Start is called before the first frame update
    void Start()
    {
        turretController = GetComponent<TurretController>();
        laserLine = GetComponent<LineRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        target = turretController.targetEnemy;
        if (target != null)
        {
            ShootLaser();
        }
        else
        {
            laserLine.enabled = false;
        }
    }

    void ShootLaser()
    {
        laserLine.enabled = true;
        laserLine.SetPosition(0, transform.position);
        laserLine.SetPosition(1, target.position);

        RaycastHit2D[] hits = Physics2D.RaycastAll(transform.position, target.position - transform.position);
        foreach (RaycastHit2D hit in hits)
        {
            if (hit.collider != null && hit.collider.CompareTag("Enemy"))
            {
                Enemy enemy = hit.collider.GetComponent<Enemy>();
                if (enemy != null)
                {
                    enemy.TakeDamage(laserDamage);
                }
            }
        }
    }
}