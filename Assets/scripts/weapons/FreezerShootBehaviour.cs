using System.Collections.Generic;
using UnityEngine;

public class FreezerShootBehaviour : ShootBehavior
{
    public TriangRange triangRange;
    public float slowDuration;
    public float slowStrength;
    private TurretController turretController;
    public Transform target;
    public Transform effectZone; // Référence à la zone d'effet

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

        // Calculez la position relative du triangle par rapport au centre de la tourelle
        Vector3 relativePosition = effectZone.position - turretController.transform.position;

        // Appliquez la rotation à la position relative
        Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        Vector3 rotatedPosition = rotation * relativePosition;

        // Mettez à jour la position du triangle en utilisant la position du centre de la tourelle et la position relative après rotation
        effectZone.position = turretController.transform.position + rotatedPosition;

        // Appliquez la rotation au triangle pour qu'il fasse face à la cible
        effectZone.rotation = rotation;
    }

    public override void Shoot()
    {
        Debug.Log("Freeze");
        // Logique spécifique au tir, si nécessaire
    }
}