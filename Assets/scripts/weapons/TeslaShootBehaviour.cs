using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeslaShootBehaviour : ShootBehavior
{
    public GameObject explosionPrefab;
    public Transform firePoint;
    public override void Shoot()
    {
        GameObject TeslaExplosion = Instantiate(explosionPrefab, firePoint.position, firePoint.rotation);
        //set damageSAmount of the explosion to the damageAmount of the turret
        //TeslaExplosion.GetComponent<teslaDamage>().damageAmount = GetComponent<CDDamageDower>().damage;
    }
}
