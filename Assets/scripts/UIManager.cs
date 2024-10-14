using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public TMP_Text BlueCoinstxt;
    public TMP_Text RedCoinstxt;
    public TMP_Text BluePV;
    public TMP_Text RedPV;
    public GameManager gameManager;
    void Start()
    {
        //get gamemanger from main camera
        gameManager = Camera.main.GetComponent<GameManager>();
    }

    void Update()
    {
        BlueCoinstxt.text = gameManager.blueCoins.ToString();
        RedCoinstxt.text = gameManager.redCoins.ToString();
        BluePV.text = gameManager.bluePV.ToString();
        RedPV.text = gameManager.redPV.ToString();
    }
}