using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TroupMovement : MonoBehaviour
{
    public enemyStats enemyStats;
    public Transform[] path;
    public int target;
    private float speed;
    public GameManager gameManager;
    public bool blueTeam;

    void Start()
    {
        Camera mainCamera = Camera.main;
        gameManager = mainCamera.GetComponent<GameManager>();

        speed = enemyStats.speed;
        // Find the GameObject named "troup path" in the scene
        GameObject troupPath = GameObject.Find("troup path");
        if (troupPath == null)
        {
            Debug.LogError("GameObject 'troup path' not found in the scene.");
            return;
        }

        // Check if the blueTeam bool is true to determine the correct path
        string teamPath = blueTeam ? "blue" : "red";

        // Find the child object for the corresponding team inside "troup path"
        Transform teamPathTransform = troupPath.transform.Find(teamPath);
        if (teamPathTransform == null)
        {
            Debug.LogError($"Child '{teamPath}' not found inside 'troup path'.");
            return;
        }

        // Get all child transforms inside the team path and assign them to the path array
        int childCount = teamPathTransform.childCount;
        path = new Transform[childCount];

        for (int i = 0; i < childCount; i++)
        {
            path[i] = teamPathTransform.GetChild(i);
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
    }

    void Update()
    {
        if (!gameManager.pause)
        {
            if (path.Length == 0) return; // If there are no waypoints, exit early

            // Move towards the target position
            Vector2 currentPosition = transform.position;
            Vector2 targetPosition = path[target].position;
            transform.position = Vector2.MoveTowards(currentPosition, targetPosition, speed * Time.deltaTime);

            // Check if the object has reached the target position
            if (currentPosition == targetPosition)
            {
                target++;

                if (target >= path.Length)
                {
                    Destroy(gameObject); // Destroy the object when it reaches the final destination
                    return;
                }
            }

            // Rotate towards the target
            Vector2 direction = targetPosition - currentPosition; // Direction to the target
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg; // Calculate angle in degrees
            transform.rotation = Quaternion.Euler(0, 0, angle - 90); // Apply rotation to the object
        }
    }
}
