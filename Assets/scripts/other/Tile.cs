using UnityEngine;

public class Tile : MonoBehaviour
{
    public GameObject particles;
    public bool activeConstruction = false;
    public float constructionTime;
    public ObjectStats objectStats;

    void Start()
    {
        // Get ObjectStats and Animator components
        objectStats = GetComponent<ObjectStats>();
        objectStats.hover = false;
        //disable the sprite renderer
        GetComponent<SpriteRenderer>().enabled = false;
    }
}