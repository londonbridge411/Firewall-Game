using System.Collections;
using System.Text;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using bowen.UI;

public class MainMenuControl : MonoBehaviour
{
    [SerializeField] Menu currentMenu;
    public Stack<Menu> menus = new Stack<Menu>();

    public static MainMenuControl instance;

    void Update()
    {
        //Controls Pausing
        if (Input.GetButtonDown("Start"))
        {
            currentMenu.CloseMenu();
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