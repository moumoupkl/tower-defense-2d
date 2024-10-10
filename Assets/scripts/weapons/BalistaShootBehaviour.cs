using UnityEngine;

public class BallistaShootBehavior : ShootBehavior
{
    public GameObject boltPrefab;
    public Transform firePoint;
    public float boltSpeed;
    public Transform target;
    private CDDamageDower cddamagetower;
    private TurretController TurretController;

    private void Start()
    {
        TurretController = GetComponent<TurretController>();
        cddamagetower = GetComponent<CDDamageDower>();
    }

    public override void Shoot()
    {
        // Instantiate and shoot bolt
        target = TurretController.targetEnemy;
        Debug.Log("shoot");
        GameObject bolt = Instantiate(boltPrefab, firePoint.position, Quaternion.identity);
        bolt.GetComponent<Bullet>().damage = cddamagetower.damage;
        Rigidbody2D rb = bolt.GetComponent<Rigidbody2D>();

        // Calculate direction and apply velocity
        Vector2 direction = (target.position - firePoint.position).normalized;
        rb.velocity = direction * boltSpeed;

        FindObjectOfType<AudioManager>().Play("Ballista_reload");
        FindObjectOfType<AudioManager>().Play("Ballista_release");
    }
}