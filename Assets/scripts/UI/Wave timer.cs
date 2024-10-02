using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wavetimer : MonoBehaviour
{
    public float waveDuration; // Duration of each wave
    public float waveSpawnDuration; // Duration of the wave end period
    public float blinkInterval; // Interval at which the fill components blink
    public float postShrinkDelay; // Delay after shrinking before starting the next wave
    public GameObject fillRight; // Right fill component
    public GameObject fillLeft; // Left fill component

    public bool spawnPhase; // Flag to indicate if the wave has ended
    private float waveTimer; // Timer for the wave duration
    private float blinkTimer; // Timer for the blinking interval
    private float postShrinkTimer; // Timer for the delay after shrinking
    private GameObject sliderRight; // Right slider component
    private GameObject sliderLeft; // Left slider component

    // Start is called before the first frame update
    void Start()
    {
        //get waveHandler component from main camera
        Camera mainCamera = Camera.main;
        WaveHandler waveHandler = mainCamera.GetComponent<WaveHandler>();
        spawnPhase = false;
        sliderRight = GameObject.Find("Slider right");
        sliderLeft = GameObject.Find("Slider left");
        waveHandler.waveNumber = 0;
        waveTimer = 0;
        blinkTimer = 0; // Initialize blink timer
        postShrinkTimer = 0; // Initialize post-shrink timer
    }

    // Update is called once per frame
    void Update()
    {
        if (!spawnPhase)
        {
            HandleWave();
        }
        else
        {
            HandleWaveEnd();
        }
    }

    // Handles the wave duration and slider updates
    private void HandleWave()
    {
        if (waveTimer < waveDuration)
        {
            waveTimer += Time.deltaTime;
            float sliderValue = waveTimer / waveDuration;
            sliderRight.GetComponent<UnityEngine.UI.Slider>().value = sliderValue;
            sliderLeft.GetComponent<UnityEngine.UI.Slider>().value = sliderValue;
        }
        else
        {
            //increase wave number
            Camera mainCamera = Camera.main;
            WaveHandler waveHandler = mainCamera.GetComponent<WaveHandler>();
            waveHandler.waveNumber++;            
            waveTimer = 0;
            spawnPhase = true;
        }
    }

    // Handles the wave end duration, blinking, shrinking, and delay logic
    private void HandleWaveEnd()
    {
        if (waveTimer < waveSpawnDuration)
        {

            waveTimer += Time.deltaTime;
            blinkTimer += Time.deltaTime;

            // Blink logic
            if (blinkTimer >= blinkInterval)
            {
                fillRight.SetActive(!fillRight.activeSelf);
                fillLeft.SetActive(!fillLeft.activeSelf);
                blinkTimer = 0; // Reset blink timer
            }

            // Shrink slider value logic
            float sliderValue = 1 - (waveTimer / waveSpawnDuration);
            sliderRight.GetComponent<UnityEngine.UI.Slider>().value = sliderValue;
            sliderLeft.GetComponent<UnityEngine.UI.Slider>().value = sliderValue;
        }
        else if (postShrinkTimer < postShrinkDelay)
        {
            postShrinkTimer += Time.deltaTime;
        }
        else
        {
            waveTimer = 0;
            spawnPhase = false;
            fillRight.SetActive(true);
            fillLeft.SetActive(true);
            sliderRight.GetComponent<UnityEngine.UI.Slider>().value = 0; // Reset slider value
            sliderLeft.GetComponent<UnityEngine.UI.Slider>().value = 0; // Reset slider value
            postShrinkTimer = 0; // Reset post-shrink timer
        }
    }
}