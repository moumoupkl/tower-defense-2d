using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimateTower : MonoBehaviour
{
    public TowerConeDetection towerConeDetection;
    private GameManager gameManager;
    public Tower tower;
    private Animator animator;

    void Start()
    {
        Camera mainCamera = Camera.main;
        gameManager = mainCamera.GetComponent<GameManager>();
        animator = GetComponent<Animator>();
        animator.SetBool("idle", true);
    }

    // Update is called once per frame
    void Update()
    {
        if (gameManager.pause)
        {
            animator.speed = 0f;
            return;
        }
        else
        {
            AnimatorClipInfo[] clipInfo = animator.GetCurrentAnimatorClipInfo(0);
            animator.speed = clipInfo[0].clip.length / tower.towerData.attackSpeed;
        }



        if (towerConeDetection.targetInSight == true)
        {
            //play attack animation, else play idle animation
            animator.SetBool("idle", false);
        }
        else
        {
            animator.SetBool("idle", true);
        }
    }
}
