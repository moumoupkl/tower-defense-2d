using UnityEngine;

public class Tile : MonoBehaviour
{
    public Animator animator;
    public GameObject particles;
    public bool activeConstruction = false;
    public float constructionTime;
    public ObjectStats objectStats;

    void Start()
    {
        // Get ObjectStats and Animator components
        objectStats = GetComponent<ObjectStats>();
        animator = GetComponent<Animator>();
        objectStats.hover = false;
    }

    void Update()
    {
        // Update animation based on hover and construction state
        animator.SetBool("ishover", objectStats.hover && !activeConstruction);
    }
}