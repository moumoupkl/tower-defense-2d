using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreezerShootBehaviour : ShootBehavior
{
    //public GameObject explosionPrefab;
    public GameObject triangle;
    private List<Collider> colliders = new List<Collider>();
    public List<Collider> GetColliders () { return colliders; }
    public Transform firePoint;
    public override void Shoot()
    {
        //GameObject TeslaExplosion = Instantiate(explosionPrefab, firePoint.position, firePoint.rotation);
        foreach (Collider enemyCollider in colliders){
            Debug.Log("qsygudfqgu");
        }
    }


}

