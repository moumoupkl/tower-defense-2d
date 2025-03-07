using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TroupMovement : MonoBehaviour
{
    // Public variables
    public enemyStats enemyStats;
    public Transform[] path;
    public int target;
    public GameManager gameManager;
    public bool blueTeam;
    public GameObject spawnParticle;
    public bool isSlowed;
    public float slowTime;
    public float slowStrength;
    public float progress;
    public Animator animator;
    private TroopPath troopPath;

    // Private variables
    private float currentSpeed;
    private float startSpeed;
    void Start()
    {
        //get trooppath from parent
        troopPath = transform.parent.GetComponent<TroopPath>();
        // Get the main camera and the GameManager component
        Camera mainCamera = Camera.main;
        gameManager = mainCamera.GetComponent<GameManager>();

        // Get the Animator component
        animator = GetComponent<Animator>();

        // Initialize speed values
        startSpeed = enemyStats.speed;
        currentSpeed = startSpeed;

        // Find the GameObject named "Troop Path" in the scene
        GameObject troupPath = troopPath.troopPath;

        // Get all child transforms inside "Troop Path" and assign them to the path array
        int childCount = troupPath.transform.childCount;
        path = new Transform[childCount];
        for (int i = 0; i < childCount; i++)
        {
            path[i] = troupPath.transform.GetChild(i);
        }

        // Initialize starting position and target index
        target = 0;
        if (path.Length > 0)
        {
            transform.position = path[target].position;
        }
        else
        {
            Debug.LogError("No waypoints found in 'path'.");
        }

        // Instantiate spawn particle effect at the starting position
        Instantiate(spawnParticle, transform.position, Quaternion.identity);
    }

    void Update()
    {
        CalculateProgress();
        // Handle slowing effect
        if (isSlowed)
        {
            currentSpeed = startSpeed * slowStrength;
            Debug.Log($"Slowed: currentSpeed = {currentSpeed}");

        }
        else
        {
            currentSpeed = startSpeed;
        }

        // If the game is not paused, move towards the target
        if (!gameManager.pause)
        {
            if (path.Length == 0) return; // If there are no waypoints, exit early

            // Move towards the target position
            Vector2 currentPosition = transform.position;
            Vector2 targetPosition = path[target].position;
            transform.position = Vector2.MoveTowards(currentPosition, targetPosition, currentSpeed * Time.deltaTime);

            // Check if the object has reached the target position
            if (Vector2.Distance(currentPosition, targetPosition) < 0.01f)
            {
                target++;
                if (target < path.Length)
                {
                    SetSideParameter(); // Call the function when reaching a new node
                }
                if (target >= path.Length)
                {
                    // Destroy the object when it reaches the final destination
                    Destroy(gameObject);
                    gameManager.DamageToPlayer(enemyStats.damage, enemyStats.blueTeam);
                    return;
                }
            }
        }
    }

    // Method to set the side parameter in the animator
    void SetSideParameter()
    {
        if (target >= path.Length) return;

        Vector2 direction = path[target].position - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        bool isMovingVertically = Mathf.Abs(angle) > 45f && Mathf.Abs(angle) < 135f;
        animator.SetBool("side", isMovingVertically);

        if (isMovingVertically)
        {
            transform.rotation = Quaternion.Euler(0, 0, angle > 0 ? 180 : 0);
            transform.localScale = Vector3.one;
        }
        else
        {
            transform.localScale = new Vector3(angle > 90 || angle < -90 ? 1 : -1, 1, 1);
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
    }

    // Method to apply a slowing effect
    public void SlowOn(float slowStrength)
    {
        Debug.Log("Applying slow effect.");
        isSlowed = true;
        this.slowStrength = slowStrength;
    }

    public void SlowOff()
    {
        isSlowed = false;
    }

    // Calculate the progress of the troop
    public void CalculateProgress()
    {
        // Calculate the progress based on the current target and the distance to the next target
        float distanceToNextTarget = Vector2.Distance(transform.position, path[target].position);
        progress = target * 1000 + (1 - distanceToNextTarget);
    }
}