using System.Collections.Generic;
using UnityEngine;

public class FreezerShootBehaviour : ShootBehavior
{
    public float slowDuration;
    public float slowStrength;
    private TurretController turretController;
    public Transform target;

    void Start()
    {
        turretController = GetComponent<TurretController>();
    }

    void Update()
    {
        target = turretController.targetEnemy;

        if (target == null)
        {
            Debug.LogWarning("No target enemy found.");
            return;
        }

        // Calculez la direction et l'angle pour faire face à la cible
        Vector3 dir = target.position - turretController.transform.position;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;

        // Ajoutez 180 degrés pour corriger le déphasage
        angle += 180;

        // Orientez le prefab (et donc le triangle) vers la cible
        turretController.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }

    public override void Shoot()
    {
        // Logique de tir ici
    }
}