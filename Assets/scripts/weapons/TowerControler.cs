using UnityEngine;

public class TowerControler : MonoBehaviour
{
    public GameManager gameManager;
    public Animator animator;
    public float constructionTime;
    public ObjectStats objectStats;


    protected virtual void Start()
    {
        //get isseelected component
        objectStats = GetComponent<ObjectStats>();
        objectStats.hover = false;
        Camera mainCamera = Camera.main;
        gameManager = mainCamera.GetComponent<GameManager>();

    }

    protected virtual void Update()
    {
        if (!gameManager.pause)
        {
   


        }
    }

}
