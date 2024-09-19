using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class teslaDamage : MonoBehaviour
{
    public int damageAmount = 10;
    public float effectDuration = 1f; // Duration of the effect in seconds
    public float blastRadius;   // Radius of the circle raycast
    public LayerMask enemyLayer;      // Layer mask to filter for enemies
    private Animator animator;
    public TurretController tc;
    private GameManager gameManager;

    private float destructionTimer;  // Tracks remaining time for destruction
    private bool isDestroyed = false; // Check if destruction was already triggered

    void Start()
    {
        Camera mainCamera = Camera.main;
        gameManager = mainCamera.GetComponent<GameManager>();
        // Get the Animator component attached to this GameObject
        animator = GetComponent<Animator>();

        AnimatorClipInfo[] clipInfo = animator.GetCurrentAnimatorClipInfo(0);
        if (clipInfo.Length > 0)
        {
            // Adjust the speed so the animation plays over the effect duration
            animator.speed = clipInfo[0].clip.length / effectDuration;
        }

        destructionTimer = effectDuration;
        ApplyDamage();
    }

    void Update()
    {
        // Check if the game is paused
        if (gameManager.pause)
        {
            // Pause the animation
            animator.speed = 0f;
            return; // Skip the rest of the Update if the game is paused
        }

        // Resume the animation when the game is not paused
        animator.speed = 1f;

        // Update the destruction timer and destroy the object when the time is up
        if (!isDestroyed)
        {
            destructionTimer -= Time.deltaTime;
            if (destructionTimer <= 0)
            {
                Destroy(gameObject);
                isDestroyed = true;
            }
        }
    }

    void ApplyDamage()
    {
        // Perform a circle raycast to find all enemies within the blast radius
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(transform.position, blastRadius, enemyLayer);

        foreach (Collider2D enemyCollider in hitEnemies)
        {
            // Access the enemy script and apply damage
            enemyStats enemy = enemyCollider.GetComponent<enemyStats>();
            enemy.TakeDamage(damageAmount);
        }
    }
}
