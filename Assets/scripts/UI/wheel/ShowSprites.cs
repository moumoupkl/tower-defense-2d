using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowSprites : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;
    public Animator animator;
    public float lerpSpeed = 5f;
    public float glowSpeed = 2f;
    public float glowMagnitude = 0.5f;
    public bool normalSize = false;
    public float timeStart = 0.0f;

    // Update is called once per frame
    void Update()
    {
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("selector_opened"))
        {
            spriteRenderer.enabled = true;
            if (normalSize)
            {
                float glow = 1 + Mathf.Sin((Time.time - timeStart + 3.14f / 2) * glowSpeed) * glowMagnitude;
                spriteRenderer.transform.localScale = new Vector3(glow, glow, 1);
            }
            else
            {
                spriteRenderer.transform.localScale = Vector3.Lerp(spriteRenderer.transform.localScale, Vector3.one, Time.deltaTime * lerpSpeed);
                //if right scale then set normalSize to true
                if (spriteRenderer.transform.localScale == Vector3.one)
                {
                    normalSize = true;
                    timeStart = Time.time;
                }
            }
        }
        else
        {
            spriteRenderer.enabled = false;
            spriteRenderer.transform.localScale = Vector3.zero;
            normalSize = false;
        }
    }
}