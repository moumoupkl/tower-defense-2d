using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class GameManager : MonoBehaviour
{
    public bool pause;
    public GameObject troup;
    public GameObject flyingtroup;
    public int currentCoins;
    public int startingCoins;
    public TMP_Text Coins;
    public UnityEngine.UI.Button summon;
    public GameObject pauseImage;
    public bool pausing;
    public bool resuming;
    private bool previousPauseState;

    // Start is called before the first frame update
    void Start()
    {
        currentCoins = startingCoins;
    }

    // Update is called once per frame
    void Update()
    {
        previousPauseState = pause;
        Coins.text = currentCoins.ToString();

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            pause = !pause;
        }

        if (pause)
        {
            pauseImage.SetActive(true);
        }
        else
        {
            pauseImage.SetActive(false);
        }

        if (previousPauseState == pause)
        {
            resuming = false;
            pausing = false;
        }

        else if (previousPauseState == false && pause == true)
        {
            pausing = true;
        }

        else
        {
            resuming = false;
        }
        
    }
    
    public void SummonTroup()
    {
        if (!pause)
        {
        Instantiate(troup, new Vector2(1000, 1000), Quaternion.identity);
        }
    }

    public void SummonFlyingTroup()
    {
        if (!pause)
        {
        Instantiate(flyingtroup, new Vector2(1000, 1000), Quaternion.identity);
        }
    }

    public void AddCoins(int coinsToAdd)
    {
        currentCoins += coinsToAdd;
    }
}
