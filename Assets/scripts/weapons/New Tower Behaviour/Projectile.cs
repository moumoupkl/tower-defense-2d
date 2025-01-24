using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    // Start is called before the first frame update
    public bool blueTeam;
    public ProjectileData projectileData;
    public Rigidbody2D rb;
    public Transform CurrentTarget;
    private float destroyTimer = 2f;
    private bool hasHitEnemy = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        Vector2 moveDirection = (CurrentTarget.position - transform.position).normalized * projectileData.bulletSpeed;

        // Apply random offset based on accuracy
        float offsetX = Random.Range(-projectileData.accuracy, projectileData.accuracy);
        float offsetY = Random.Range(-projectileData.accuracy, projectileData.accuracy);
        moveDirection += new Vector2(offsetX, offsetY);

        rb.velocity = new Vector2(moveDirection.x, moveDirection.y);
        destroyTimer = projectileData.bulletLifetime;
    }

    // Update is called once per frame
    void Update()
    {
        float angle = Mathf.Atan2(rb.velocity.y, rb.velocity.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward) * Quaternion.Euler(0, 0, 180); ;

        destroyTimer -= Time.deltaTime;
        if (destroyTimer <= 0f)
        {
            Impact();
        }
    }

    void OnTriggerEnter2D(Collider2D hitInfo)
    {
        //check if the projectile hit the target
        if (hasHitEnemy == false && hitInfo.CompareTag("Enemy"))
        {
            //return if the enemy is on the same team
            enemyStats enemy = hitInfo.GetComponent<enemyStats>();
            if (enemy != null && enemy.blueTeam == blueTeam)
            {
                return;
            }
            hasHitEnemy = true; //set the flag to true to prevent multiple hits

            // Access the enemy script and apply damage
            if (enemy != null)
            {
                enemy.TakeDamage(projectileData.bulletDamage);
            }

            // Play the impact routine
            Impact();
        }
    }

    void Impact()
    {
        //stop gameobject, disable collider, turn off sprite, destroy after 1 second
        GetComponent<Collider2D>().enabled = false;
        rb.velocity = Vector2.zero;
        GetComponent<SpriteRenderer>().enabled = false;
        Destroy(gameObject, 1f);
    }
}