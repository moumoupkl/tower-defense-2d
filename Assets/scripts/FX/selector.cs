using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class selector : MonoBehaviour
{
    public bool hover;
    //own animator
    private Animator animator;
    //child gameobject
    public GameObject child;
    //child animator
    private Animator childAnimator;


    void Start()
    {
        animator = GetComponent<Animator>();
        //get child animator
        childAnimator = child.GetComponent<Animator>();
        //hide childgameobject
        child.SetActive(false);

    }

    void Update()
    {
        //if hover, change iddle bool from animator to false
        if (hover)
        {
            animator.SetBool("iddle", false);
            //show child gameobject and play animation
            child.SetActive(true);
            //start animation from begining
            childAnimator.Play("tile glow", 0);


        }
        else
        {
            animator.SetBool("iddle", true);
            //hide child gameobject
            child.SetActive(false);
        }

    }
}
