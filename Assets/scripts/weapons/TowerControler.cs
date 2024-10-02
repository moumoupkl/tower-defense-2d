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
        //say if gamemanager is null

    }

    protected virtual void Update()
    {
        Debug.Log("TowerController Update");
        if (!gameManager.pause)
        {
   


        }
    }

}
