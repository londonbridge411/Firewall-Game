using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Events;
using UnityEngine.UI;
using bowen.UI;
using TMPro;

public class VideoMenu : Menu
{
    public static VideoMenu instance;

    public TMP_Dropdown resolutionDropdown;
    public TMP_Dropdown qualityDropdown;
    public TMP_Dropdown textureDropdown;
    public TMP_Dropdown aaDropdown;
    public TMP_Dropdown fpsDropdown;
    public Toggle fullscreen;
    public Toggle vSync;
    public Resolution[] resolutions;

    public int isFullScreen { get; private set; }
    public int vSyncOn { get; private set; }

    private void Awake()
    {
        if (instance == null)
            instance = this;
    }

    private void Start()
    {
        Load();
        resolutions = Screen.resolutions.Select(resolution => new Resolution { width = resolution.width, height = resolution.height }).Distinct().ToArray();

        resolutionDropdown.ClearOptions();

        List<string> options = new List<string>();

        int currentResolutionIndex = 1;
        for (int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + " x " + resolutions[i].height;
            options.Add(option);

            if (resolutions[i].width == Screen.currentResolution.width && resolutions[i].height == Screen.currentResolution.height)
            {
                currentResolutionIndex = i;
            }
        }
        resolutionDropdown.AddOptions(options);
        resolutionDropdown.value = currentResolutionIndex;
        resolutionDropdown.RefreshShownValue();



        Load();
        vSync.isOn = (vSyncOn == 1) ? true : false;
        fullscreen.isOn = (isFullScreen == 1) ? true : false;
    }

    public void SetQuality(int qualityIndex)
    {
        QualitySettings.SetQualityLevel(qualityIndex);
    }

    public void SetResolution(int resolutionIndex)
    {
        Resolution resolution = resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }

    public void SetAntiAliasing(int aaIndex)
    {
        QualitySettings.antiAliasing = aaIndex;
    }

    public void SetFullscreen(bool isFullScreen)
    {
        Screen.fullScreen = isFullScreen;
        this.isFullScreen = fullscreen.isOn ? 1 : 0;
    }

    public void CapFPS()
    {
        switch (fpsDropdown.value)
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

    public void SetVsync(bool isOn)
    {
        QualitySettings.vSyncCount = isOn ? 1 : 0;
        vSyncOn = vSync.isOn ? 1 : 0;
    }

    public void Save()
    {
        PlayerPrefs.SetInt("Quality", qualityDropdown.value);
        PlayerPrefs.SetInt("Resolution", resolutionDropdown.value);
        PlayerPrefs.SetInt("Texture", textureDropdown.value);
        PlayerPrefs.SetInt("Anti-Aliasing", aaDropdown.value);
        PlayerPrefs.SetInt("FPS", fpsDropdown.value);
        PlayerPrefs.SetInt("vSync", vSyncOn);
        PlayerPrefs.SetInt("Fullscreen", isFullScreen);
    }

    public void Load()
    {
        qualityDropdown.value = PlayerPrefs.GetInt("Quality");
        resolutionDropdown.value = PlayerPrefs.GetInt("Resolution");
        textureDropdown.value = PlayerPrefs.GetInt("Texture");
        aaDropdown.value = PlayerPrefs.GetInt("Anti-Aliasing");
        fpsDropdown.value = PlayerPrefs.GetInt("FPS");
        vSyncOn = PlayerPrefs.GetInt("vSync");
        isFullScreen = PlayerPrefs.GetInt("Fullscreen");
    }

    public override void Execute()
    {
        throw new System.NotImplementedException();
    }

    public override void OnEnter()
    {
        throw new System.NotImplementedException();
    }

    public override void OnExit()
    {
        if (instance != null)
        {
            instance.Save();
            print("Video Settings Saved.");
        }
    }
}
