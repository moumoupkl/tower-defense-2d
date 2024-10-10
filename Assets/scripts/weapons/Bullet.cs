using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private float destroyAfterSeconds = 2f;
    public int damage;
    private bool hasHitEnemy = false; // Flag to track if the bullet has hit an enemy
    private GameManager gameManager;
    private Rigidbody2D rb;
    private Vector2 currentVelocity;
    private float destroyTimer; // Timer for bullet destruction

    void Start()
    {
        // Get the GameManager from the main camera
        Camera mainCamera = Camera.main;
        gameManager = mainCamera.GetComponent<GameManager>();

        // Get the Rigidbody2D component
        rb = GetComponent<Rigidbody2D>();

        // Initialize initial velocity
        currentVelocity = rb.velocity;

        // Initialize the destroy timer
        destroyTimer = destroyAfterSeconds;
    }

    void Update()
    {
        // Check if the game is paused
        if (gameManager.pause) // Use the correct pause state
        {
            // Stop the bullet's movement
            rb.velocity = Vector2.zero;
        }
        else
        {
            // Resume movement by reapplying the initial velocity
            if (rb.velocity == Vector2.zero) // Only set initial velocity if it's zero
            {
                rb.velocity = currentVelocity;
            }
            currentVelocity = rb.velocity;
            //rotate the bullet torwords the target enemy
            if (rb.velocity != Vector2.zero)
            {
                float angle = Mathf.Atan2(rb.velocity.y, rb.velocity.x) * Mathf.Rad2Deg;
                transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
            }

            // Update the destroy timer if not paused
            destroyTimer -= Time.deltaTime;
            if (destroyTimer <= 0f)
            {
                Destroy(gameObject);
            }
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (!hasHitEnemy && collision.CompareTag("Enemy"))
        {
            // Set the flag to true to prevent further damage
            hasHitEnemy = true;

            // Access the enemy script and apply damage
            enemyStats enemy = collision.GetComponent<enemyStats>();
            if (enemy != null)
            {
                enemy.TakeDamage(damage);
            }

            // Destroy the bullet
            Destroy(gameObject);
        }
    }
}
