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

    public override void OnEnter()
    {
        
    }

    public override void OnExit()
    {

    }

    public override void Execute()
    {
        
    }
}
