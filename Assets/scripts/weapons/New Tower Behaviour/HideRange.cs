using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideRange : MonoBehaviour
{
    public ObjectStats objectStats;
    private SpriteRenderer spriteRenderer;
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.enabled = false;
    }
    // Update is called once per frame
    void Update()
    {
        if (objectStats.hover == true)
        {
            spriteRenderer.enabled = true;
        }
        else
        {
            spriteRenderer.enabled = false;
        }
    }
}
