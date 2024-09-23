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
    public GameObject troup;
    public GameObject flyingtroup;
    public int blueCoins;
    public float bluecoinspersecs;
    private float bluecoinsfloat;
    public int redCoins;
    public float redcoinspersecs;
    private float redcoinsfloat;
    public int startingCoins;
    public TMP_Text BlueCoinstxt;
    public TMP_Text RedCoinstxt;
    public UnityEngine.UI.Button summon;
    public GameObject pauseImage;
    public bool pausing;
    public bool resuming;
    private bool previousPauseState;
    public InputActionReference move1;
    public InputActionReference move2;

    // Start is called before the first frame update
    void Start()
    {
        blueCoins = startingCoins;
        redCoins = startingCoins;
    }

    // Update is called once per frame
    void Update()
    {
        previousPauseState = pause;
        BlueCoinstxt.text = blueCoins.ToString();
        RedCoinstxt.text = redCoins.ToString();

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
    public void GroundTroupBlue()
    {
        if (!pause)
        {
        Instantiate(troup, new Vector2(1000, 1000), Quaternion.identity);
        }
    }

    public void FlyingTroupBlue()
    {
        if (!pause)
        {
        Instantiate(flyingtroup, new Vector2(1000, 1000), Quaternion.identity);
        }
    }

    // --------------------- red team ---------------------
    

    public void AddCoins(int coinsToAdd, bool blueTeam)
    {
        if (blueTeam)
            bluecoinsfloat += coinsToAdd;
        else
            redcoinsfloat += coinsToAdd;
    }

}
