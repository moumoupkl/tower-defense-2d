using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeslaController : MonoBehaviour
{
    public Transform gun; // The part of the turret that rotates, e.g., a barrel or gun
    public GameObject explosionPrefab; // Bullet prefab to be spawned
    public Transform firePoint; // Where the bullet will be spawned
    public float range = 10f; // Detection range for enemies
    public float fireCooldown = 1f; // Cooldown time between shots in seconds
    public GameObject rangeIndicator; // Reference to the range indicator (circle)
    private Transform targetEnemy;
    private SpriteRenderer rangeRenderer;
    private float fireTimer; // Timer to track cooldown
    public GameManager gameManager;
    public Animator animator;
    private Animator weaponAnimator; // Reference to the weapon's Animator
    public bool hover;

    void Start()
    {
        Camera mainCamera = Camera.main;
        gameManager = mainCamera.GetComponent<GameManager>();

        // Set up the range indicator
        if (rangeIndicator != null)
        {
            rangeRenderer = rangeIndicator.GetComponent<SpriteRenderer>();
            rangeIndicator.transform.localScale = new Vector3(range * 2, range * 2, 1); // Adjust to match range
            rangeRenderer.enabled = false; // Hide range indicator by default
        }

        // Get the Animator component from the child object named "weapon"
        Transform weapon = transform.Find("weapon");
        if (weapon != null)
        {
            weaponAnimator = weapon.GetComponent<Animator>();
        }

        fireTimer = fireCooldown; // Initialize the timer
    }

    void Update()
    {

        // Set the animation speed to play over the duration of the lifetime
        AnimatorClipInfo[] clipInfo = weaponAnimator.GetCurrentAnimatorClipInfo(0);
        if (clipInfo.Length > 0)
        {
            // Set animator speed so the animation plays over the entire lifetime
            weaponAnimator.speed = clipInfo[0].clip.length / fireCooldown;
        }

        if (!gameManager.pause)
        {
            // Update the cooldown timer
            fireTimer += Time.deltaTime;

            // Find the closest enemy within range
            FindClosestEnemy();

            if (targetEnemy != null)
            {
                animator.SetBool("fight", true);
                // Rotate the turret to face the closest enemy before shooting

                // Check if the enemy is within range and shoot if the cooldown has passed
                float distanceToEnemy = Vector2.Distance(transform.position, targetEnemy.position);
                if (distanceToEnemy <= range && fireTimer >= fireCooldown)
                {
                    Shoot();
                    fireTimer = 0f; // Reset the cooldown timer
                }
            }
            else 
            {
                animator.SetBool("fight", false);
            }
        }
        else
        {
            animator.speed = 0;
        }
        selected();
    }

    void FindClosestEnemy()
    {
        // Find all enemies in the scene
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        float closestDistance = Mathf.Infinity;
        Transform closestEnemy = null;

        // Iterate through all enemies to find the closest one within range
        foreach (GameObject enemy in enemies)
        {
            float distanceToEnemy = Vector2.Distance(transform.position, enemy.transform.position);
            if (distanceToEnemy < closestDistance && distanceToEnemy <= range)
            {
                closestDistance = distanceToEnemy;
                closestEnemy = enemy.transform;
            }
        }

        // Set the closest enemy as the target
        targetEnemy = closestEnemy;
    }

    void Shoot()
    {
        // Instantiate the bullet at the fire point
        GameObject bullet = Instantiate(explosionPrefab, firePoint.position, firePoint.rotation);
    }

    // Show the range indicator when the mouse hovers over the turret
    void selected()
    {
        if (hover)
        {
            if (rangeRenderer != null)
            {
                rangeRenderer.enabled = true;
            }
        }

        else
        {
            if (rangeRenderer != null)
            {
                rangeRenderer.enabled = false;
            }
        }

    }
}