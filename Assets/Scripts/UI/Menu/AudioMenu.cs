using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Events;
using UnityEngine.UI;
using bowen.UI;

public class AudioMenu : Menu
{
    public AudioMixer audioMixer;
    public float masterVolume;
    public float musicVolume;
    public float effectsVolume;
    public Slider master;
    public Slider music;
    public Slider effects;

    #region Singleton
    public static AudioMenu instance;

    void Awake()
    {
        if (instance == null)
            instance = this;
    }
    #endregion

    void Start()
    {
        Load();
    }

    //Don't forget to have audio settings
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
            Save();
            print("Saved audio settings.");
        }
    }

    public void SetMasterVolume(float volume)
    {
        audioMixer.SetFloat("MasterVolume", volume);
        masterVolume = volume;
    }

    public void SetMusicVolume(float volume)
    {
        audioMixer.SetFloat("MusicVolume", volume);
        musicVolume = volume;
    }

    public void SetEffectsVolume(float volume)
    {
        audioMixer.SetFloat("EffectsVolume", volume);
        effectsVolume = volume;
    }

    public void ResetDefault()
    {

        audioMixer.SetFloat("MasterVolume", 0);
        audioMixer.SetFloat("MusicVolume", 0);
        audioMixer.SetFloat("EffectsVolume", 0);

        Slider[] sliders = GetComponentsInChildren<Slider>();
        foreach (Slider s in sliders)
        {
            s.value = 0f;
        }
    }

    public void Save()
    {
        PlayerPrefs.SetFloat("MasterVolume", masterVolume);
        PlayerPrefs.SetFloat("MusicVolume", musicVolume);
        PlayerPrefs.SetFloat("EffectsVolume", effectsVolume);
    }

    public void Load()
    {
        master.value = PlayerPrefs.GetFloat("MasterVolume");
        music.value = PlayerPrefs.GetFloat("MusicVolume");
        effects.value = PlayerPrefs.GetFloat("EffectsVolume");
    }
}
