using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using bowen.UI;

public class PauseMenu : Menu
{
    #region Singleton
    public static PauseMenu instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
            Destroy(gameObject);
    }
    #endregion

    void Start()
    {
        //CloseMenu();
    }

    public void Pause()
    {
        OpenMenu();
        AudioManager.instance.Pause(AudioManager.instance.backgroundMusic);
        MenuControl.instance.isPaused = true;
        Time.timeScale = 0f;
    }

    public void Resume()
    {
        AudioManager.instance.Play(AudioManager.instance.backgroundMusic);
        if (MenuControl.instance.menus.Count == 1)
        {
            MenuControl.instance.isPaused = false;
            Time.timeScale = 1f;
            CloseMenu();
        }

    }

    public override void OnEnter()
    {
        throw new System.NotImplementedException();
    }

    public override void OnExit()
    {
        print("Resuming game.");
    }

    public override void Execute()
    {
        throw new System.NotImplementedException();
    }
}
