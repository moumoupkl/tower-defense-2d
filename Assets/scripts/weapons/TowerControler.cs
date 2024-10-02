using UnityEngine;

public class TowerControler : MonoBehaviour
{
    public GameManager gameManager;
    public Animator animator;
    public float constructionTime;
    public ObjectStats objectStats;


    public virtual void Start()
    {
        //get isseelected component
        objectStats = GetComponent<ObjectStats>();
        objectStats.hover = false;
        Camera mainCamera = Camera.main;
        gameManager = mainCamera.GetComponent<GameManager>();
        //say if gamemanager is null

    }

    public virtual void Update()
    {
        Debug.Log("TowerController Update");
        if (!gameManager.pause)
        {
   


        }
    }

}
