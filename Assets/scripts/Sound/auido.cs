using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class audio : MonoBehaviour
{
    public float volume;
    private AudioManager audioManager;
    void Start()
    {
        audioManager = FindObjectOfType<AudioManager>(); //find the audio manager component

        //multiply the volume of all the audios in the sounds variable in the audio manager component by the volume variable
        foreach (Sound s in audioManager.sounds)
        {
            s.source.volume = volume;
        }


    }

    // Update is called once per frame
    void Update()
    {

    }
}
