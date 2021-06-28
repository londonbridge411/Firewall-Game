using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Events;
using UnityEngine.UI;
using TMPro;
using bowen.Saving;
using System.Linq;

public class SettingsData : MonoBehaviour
{
    public static SettingsData instance;
    public AudioMixer audioMixer;
    void Start() //Try start if there are any problems
    {
        //Audio
        audioMixer.SetFloat("MasterVolume", PlayerPrefs.GetFloat("MasterVolume"));
        audioMixer.SetFloat("MusicVolume", PlayerPrefs.GetFloat("MusicVolume"));
        audioMixer.SetFloat("EffectsVolume", PlayerPrefs.GetFloat("EffectsVolume"));

        //Video
        Resolution[] resolutions = Screen.resolutions.Select(resolution => new Resolution { width = resolution.width, height = resolution.height }).Distinct().ToArray(); //Saves and Loads
        Resolution resolution2 = resolutions[PlayerPrefs.GetInt("Resolution")];
        Screen.SetResolution(resolution2.width, resolution2.height, Screen.fullScreen);

        QualitySettings.SetQualityLevel(PlayerPrefs.GetInt("Quality")); //Saves and Loads
        Screen.fullScreen = (PlayerPrefs.GetInt("Fullscreen") == 1) ? true : false; //Saves and Loads
        QualitySettings.vSyncCount = PlayerPrefs.GetInt("vSync"); //Saves and Loads
        QualitySettings.antiAliasing = PlayerPrefs.GetInt("Anti-Aliasing");
        SetFPS(PlayerPrefs.GetInt("FPS")); //Saves and Loads

    }

    void Update()
    {
        if (Screen.fullScreen)
            print("SET TO FULLSCREEN!");
        if (QualitySettings.vSyncCount == 1)
            print("VSYNC ON!");
    }

    public void SetFPS(int value)
    {
        switch (value)
        {
            case 0:
                Application.targetFrameRate = 30;
                break;
            case 1:
                Application.targetFrameRate = 60;
                break;
            case 2:
                Application.targetFrameRate = 120;
                break;
            case 3:
                Application.targetFrameRate = 144;
                break;
            case 4:
                Application.targetFrameRate = -1;
                break;
        }
    }
}
