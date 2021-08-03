using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using bowen.Saving;
using bowen.UI;
using UnityEngine.SceneManagement;

public class MainMenu : Menu
{
    private void Start()
    {
        SaveLoadSystem.instance.Load();
    }

    public void Continue()
    {
       // MenuControl.instance.CloseMenu();
        SceneManager.LoadScene(1);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void OnEnter()
    {
        throw new System.NotImplementedException();
    }

    public override void OnExit()
    {
        print("Exited Main Menu.");
    }

    public override void Execute()
    {
        throw new System.NotImplementedException();
    }
}

public class StartButton : MonoBehaviour
{

}
