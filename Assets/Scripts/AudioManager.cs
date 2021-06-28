using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Audio;
using UnityEngine;


public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;
    public string backgroundMusic;
    public Sound[] sounds;
    
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.outputAudioMixerGroup = s.mixerGroup;

            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
        }
    }

    private void Start()
    {
        
    }

    private void Update()
    {

    }

    public void Play(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
        {
            Debug.LogWarning("Sound name could not be found!");
            return;
        }

        s.source.Play();
    }

    public void PlayOneShot(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
        {
            Debug.LogWarning("Sound name could not be found!");
            return;
        }

        s.source.PlayOneShot(s.clip);
    }

    public void Resume(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
        {
            Debug.LogWarning("Sound name could not be found!");
            return;
        }
    }

    public void Stop(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
        {
            Debug.LogWarning("Sound name could not be found!");
            return;
        }

        s.source.Stop();
    }

    public void Pause(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
        {
            Debug.LogWarning("Sound name could not be found!");
            return;
        }

        s.source.Pause();
    }

    public void UnPause(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
        {
            Debug.LogWarning("Sound name could not be found!");
            return;
        }

        s.source.UnPause();
    }

    private Sound GetSound(string name)
    {
        return Array.Find(sounds, sound => sound.name == name);
    }

    public void SwapSong(string s)
    {
        StopAllCoroutines();
        StartCoroutine(Transition(s));
    }

    public IEnumerator Transition(string name)
    {
        float fadeTime = 2f;
        float elapsedTime = 0;

        string lastBGM = backgroundMusic;
        backgroundMusic = name;

        Play(name);
        while (elapsedTime < fadeTime)
        {
            GetSound(backgroundMusic).source.volume = Mathf.Lerp(0, 1, elapsedTime / fadeTime);
            if (lastBGM != string.Empty)
            {
                GetSound(lastBGM).source.volume = Mathf.Lerp(1, 0, elapsedTime / fadeTime);
            }
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        if (backgroundMusic != string.Empty)
        {
            Stop(lastBGM);
        }
    }
}
