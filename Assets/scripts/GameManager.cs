using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public bool pause;
    public bool gameisover;
    public int blueCoins;
    public float bluecoinspersecs;
    public float bluecoinsfloat;
    public int bluePV = 100;
    public int redCoins;
    public float redcoinspersecs;
    public float redcoinsfloat;
    public int redPV = 100;
    public int startingCoins;
    public TMP_Text BlueCoinstxt;
    public TMP_Text RedCoinstxt;
    public TMP_Text Winner;
    public GameObject gameover;
    public TMP_Text BluePV;
    public TMP_Text RedPV;
    public GameObject pauseImage;
    public InputActionReference move1;
    public InputActionReference move2;

    private bool previousPauseState;

    void Start()
    {
        bluecoinsfloat = startingCoins;
        redcoinsfloat = startingCoins;
        gameover.SetActive(false);
        Cursor.visible = false;
    }

    void Update()
    {
        BlueCoinstxt.text = blueCoins.ToString();
        RedCoinstxt.text = redCoins.ToString();
        BluePV.text = bluePV.ToString();
        RedPV.text = redPV.ToString();

        // Toggle pause on Escape key
        if (Input.GetKeyDown(KeyCode.Escape) && !gameisover)
        {
            pause = !pause;
        }

        // Show/hide pause image
        pauseImage.SetActive(pause && !gameisover);

        // Detect pausing or resuming
        if (previousPauseState != pause)
        {
            if (pause)
                Debug.Log("Game Paused");
            else
                Debug.Log("Game Resumed");
        }
        previousPauseState = pause;

        // Enable move actions if disabled
        if (!move1.action.enabled || !move2.action.enabled)
        {
            move1.action.Enable();
            move2.action.Enable();
        }

        // Update coins over time
        bluecoinsfloat += bluecoinspersecs * Time.deltaTime;
        redcoinsfloat += redcoinspersecs * Time.deltaTime;

        blueCoins = (int)bluecoinsfloat;
        redCoins = (int)redcoinsfloat;

        // Reload scene on 'R' key if the game is over
        if (gameisover && Input.GetKeyDown(KeyCode.R))
        {
            ReloadCurrentScene();
        }
    }

    // Add coins to the respective team
    public void AddCoins(int coinsToAdd, bool blueTeam)
    {
        if (blueTeam)
            bluecoinsfloat += coinsToAdd;
        else
            redcoinsfloat += coinsToAdd;
    }

    // Apply damage to the player, check for game over
    public void DamageToPlayer(int damage, bool blueTeam)
    {
        if (!blueTeam)
        {
            bluePV = Mathf.Max(0, bluePV - damage);
            if (bluePV == 0) GameOver(blueTeam);
        }
        else
        {
            redPV = Mathf.Max(0, redPV - damage);
            if (redPV == 0) GameOver(blueTeam);
        }
    }

    // Handle game over state
    public void GameOver(bool blueTeam)
    {
        Winner.text = blueTeam ? "Blue Team Wins" : "Red Team Wins";
        gameover.SetActive(true);
        gameisover = true;
        pause = true;
    }

    // Reload the current scene
    public void ReloadCurrentScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
