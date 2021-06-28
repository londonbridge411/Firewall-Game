using System.Collections;
using System.Text;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using bowen.UI;

public class MenuControl : MonoBehaviour
{
    [SerializeField] Menu currentMenu;
    public PauseMenu pauseMenu;
    public Stack<Menu> menus = new Stack<Menu>();
    public bool isPaused;

    //Main Menus
    //Inventory Menus
    //Pause Menu

    #region Singleton
    public static MenuControl instance;

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
                    currentMenu.CloseMenu();
            }
            else
            {
                currentMenu.CloseMenu();
            }
        }

        if (menus.Count > 0)
        {
            currentMenu = menus.Peek();
            print(StackToString());
        }
        else
        {
            currentMenu = null;
        }
    }

    private string StackToString()
    {
        StringBuilder sb = new StringBuilder();
        foreach (Menu m in menus)
        {
            sb.Append(m + ", ");
        }
        return sb.ToString();
    }

    public void SwitchMenu(Menu menu)
    {
        currentMenu = menu;
        currentMenu.OpenMenu();
    }

    public void CloseMenu()
    {
        currentMenu.CloseMenu();
    }
}
