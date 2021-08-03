using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class CharMenuControl : MonoBehaviour
{
    public GameObject currentMenu;
    public PauseMenu pauseMenu;
    public bool isPaused;

    public void SwitchMenu(GameObject menu)
    {
        if (currentMenu != null)
            currentMenu.SetActive(false);

        currentMenu = menu;
        currentMenu.SetActive(true);
    }

    public void CloseMenu()
    {
        if (currentMenu != null)
            currentMenu.SetActive(false);
    }

    #region Singleton
    public static CharMenuControl instance;

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

    void Update()
    {
        //Controls Pausing
        if (Input.GetButtonDown("Start"))
        {
            if (SceneManager.GetActiveScene().buildIndex != 0)
            {
                if (currentMenu == null)
                {
                    pauseMenu.Pause();
                }
                else if (currentMenu == pauseMenu)
                {
                    pauseMenu.Resume();
                }
                else
                    currentMenu.SetActive(false);
            }
            else
            {
                currentMenu.SetActive(false);
            }
        }

        /*if (menus.Count > 0)
        {
            currentMenu = menus.Peek();
            print(StackToString());
        }
        else
        {
            currentMenu = null;
        }*/
    }
}
