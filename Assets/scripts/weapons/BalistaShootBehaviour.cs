using UnityEngine;

public class BallistaShootBehavior : ShootBehavior
{
    public GameObject boltPrefab;
    public Transform firePoint;
    public float boltSpeed;

    public override void Shoot()
    {
        // Instantiate and shoot bolt
        Debug.Log("shoot");
        GameObject bolt = Instantiate(boltPrefab, firePoint.position, firePoint.rotation);
        Rigidbody2D rb = bolt.GetComponent<Rigidbody2D>();
        rb.velocity = -firePoint.right * boltSpeed;
        FindObjectOfType<AudioManager>().Play("Ballista_reload");
        FindObjectOfType<AudioManager>().Play("Ballista_release");
    }
}
