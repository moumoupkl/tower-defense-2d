using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using UnityEngine.UIElements;


public class GameManager : MonoBehaviour
{
    public bool pause;
    public bool gameisover;
    public int blueCoins;
    public float bluecoinspersecs;
    public float bluecoinsfloat;
    private int bluePV= 100;
    public int redCoins;
    public float redcoinspersecs;
    public float redcoinsfloat;
    private int redPV= 100;
    public int startingCoins;
    public TMP_Text BlueCoinstxt;
    public TMP_Text RedCoinstxt;
    public TMP_Text Winner;
    public GameObject gameover;
    public TMP_Text BluePV;
    public TMP_Text RedPV;
    public GameObject pauseImage;
    public bool pausing;
    public bool resuming;
    private bool previousPauseState;
    public InputActionReference move1;
    public InputActionReference move2;

    // Start is called before the first frame update
    void Start()
    {
        bluecoinsfloat = startingCoins;
        redcoinsfloat = startingCoins;

        gameover.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        previousPauseState = pause;
        BlueCoinstxt.text = blueCoins.ToString();
        RedCoinstxt.text = redCoins.ToString();
        BluePV.text = bluePV.ToString();
        RedPV.text = redPV.ToString();

        if (Input.GetKeyDown(KeyCode.Escape) && !gameisover)
        {
            pause = !pause;
        }

        if (pause && !gameisover)
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

        if (!move1.action.enabled || !move2.action.enabled)
        {
            move1.action.Enable();
            move2.action.Enable();
            Debug.Log("Re-enabled move action after tile deactivation.");
        }

        bluecoinsfloat += bluecoinspersecs * Time.deltaTime;
        redcoinsfloat += redcoinspersecs * Time.deltaTime;

        blueCoins = (int) bluecoinsfloat;
        redCoins = (int) redcoinsfloat;
        
    }
    // --------------------- blue team ---------------------
    
    public void AddCoins(int coinsToAdd, bool blueTeam)
    {
        if (blueTeam)
            bluecoinsfloat += coinsToAdd;
        else
            redcoinsfloat += coinsToAdd;
    }

    public void DamageToPlayer(int Damage, bool blueTeam)
    {
        if (!blueTeam){
            if(bluePV-Damage >= 0)
                bluePV -= Damage;
            else
            {
                bluePV=0;
                GameOver(blueTeam);
            }
                
            
        }
        else
            if(redPV-Damage >= 0)
                redPV -= Damage;
            else
            {
                redPV=0;
                GameOver(blueTeam);
            }
            
    }

    public void GameOver(bool blueTeam)
    {
        if(!blueTeam)
            Winner.text = "Red Team Wins";

        else
            Winner.text = "Blue Team Wins";

        gameover.SetActive(true);
        gameisover = true;
        pause = true;
    }


}

