using System;
using System.Collections;
using System.Text;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using bowen.UI;

public class MenuControl : MonoBehaviour
{
    #region Singleton
    public static MenuControl instance;

    private void Awake()
    {
        if (instance != null && instance != this)
            Destroy(this.gameObject);
        else
            instance = this;
    }
    #endregion

    public Menu currentMenu;
    public Menu lastMenu;
    public PauseMenu pauseMenu;
    public bool isPaused;

    public void SwitchMenu(Menu menu)
    {

        if (currentMenu != null)
            currentMenu.CloseMenu();

        if (menu == PauseMenu.instance || menu == ItemMenu.instance) //Add characters menu
            lastMenu = null;
        else if (menu == OptionsMenu.instance)
            lastMenu = PauseMenu.instance;
        else if (menu == VideoMenu.instance || menu == AudioMenu.instance) //Add control menu
            lastMenu = OptionsMenu.instance;
        else
            lastMenu = currentMenu;

        currentMenu = menu;
        try
        {
            currentMenu.OpenMenu();
        }
        catch (NullReferenceException)
        {

        }
    }

    private void CloseMenu()
    {
        if (currentMenu != null)
        {    
            SwitchMenu(lastMenu);
        }
    }

    void Update()
    {
        //Controls Pausing
        if (Input.GetButtonDown("Start"))
        {
            if (SceneManager.GetActiveScene().buildIndex != 0)
            {
                if (currentMenu == null)
                {
                    SwitchMenu(pauseMenu);
                    currentMenu.Pause();
                }
                else if (currentMenu == pauseMenu || currentMenu == ItemMenu.instance) //Add character menu
                {
                    Resume();
                }
                else
                {
                    CloseMenu();
                }
            }
        }
    }

    public void Resume()
    {
        currentMenu.Resume();
        SwitchMenu(null);
    }
}
