using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimateCoil : MonoBehaviour
{
    public TowerConeDetection towerConeDetection;
    private GameManager gameManager;
    public Tower tower;
    private Animator animator;
    public float idleTime = 1f;
    public float attackTime = 0.5f;
    public float actualAnimationSpeed = 1f;
    public float targetAnimationSpeed = 1f;

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
        }
        else
        {
            if (towerConeDetection.targetInSight == true)
            {
                //scale the speed 
                targetAnimationSpeed = attackTime;
            }
            else
            {
                targetAnimationSpeed = idleTime;
            }
            //lerp actualanimationspeed to targetanimationspeed
            actualAnimationSpeed = Mathf.Lerp(actualAnimationSpeed, targetAnimationSpeed, Time.deltaTime * 5);
            AnimatorClipInfo[] clipInfo = animator.GetCurrentAnimatorClipInfo(0);
            animator.speed = clipInfo[0].clip.length / actualAnimationSpeed;
        }

    }
}
