using UnityEngine;

public class AnimationEffect : MonoBehaviour
{
    public float lifetime = 2f;
    private Animator animator;

    void Start()
    {
        // Get the Animator component attached to this GameObject
        animator = GetComponent<Animator>();

        // Set the animation speed to play over the duration of the lifetime
        AnimatorClipInfo[] clipInfo = animator.GetCurrentAnimatorClipInfo(0);
        if (clipInfo.Length > 0)
        {
            // Set animator speed so the animation plays over the entire lifetime
            animator.speed = clipInfo[0].clip.length / lifetime;
        }

        // Destroy the explosion GameObject after the lifetime duration
        Destroy(gameObject, lifetime);
    }
}
