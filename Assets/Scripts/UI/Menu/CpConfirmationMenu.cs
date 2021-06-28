using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using bowen.UI;
public class CpConfirmationMenu : Menu
{
    public void LoadLastPoint()
    {
        SceneManager.LoadScene(1);
        MenuControl.instance.CloseMenu();
        PauseMenu.instance.Resume();
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
