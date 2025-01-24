using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoilExplosion : MonoBehaviour
{
    public float effectDuration = 1f; // Duration of the effect in seconds
    private Animator animator;
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
}