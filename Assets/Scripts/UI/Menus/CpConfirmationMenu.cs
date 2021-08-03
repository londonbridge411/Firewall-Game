using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using bowen.UI;
using bowen.Saving;

public class CpConfirmationMenu : Menu
{
    #region Singleton
    public static CpConfirmationMenu instance;

    private void Awake()
    {
        if (instance == null)
            instance = this;
    }
    #endregion

    public void LoadLastPoint()
    {
        SceneManager.LoadScene(1);
        //InventoryManager.instance.inventory.Clear();
        SaveLoadSystem.instance.Load();
        MenuControl.instance.SwitchMenu(MenuControl.instance.lastMenu);
        MenuControl.instance.Resume();
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
        print("Confirmed");
    }
}
