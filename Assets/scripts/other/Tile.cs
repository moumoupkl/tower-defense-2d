using UnityEngine;

public class Tile : MonoBehaviour
{
    public GameObject particles;
    public bool activeConstruction = false;
    public float constructionTime;
    public ObjectStats objectStats;
    public bool hideTiles = true;

    void Start()
    {
        // Get ObjectStats and Animator components
        objectStats = GetComponent<ObjectStats>();
        objectStats.hover = false;
        //disable the sprite renderer
        if (hideTiles)
        {
            GetComponent<SpriteRenderer>().enabled = false;
        }
    }
}