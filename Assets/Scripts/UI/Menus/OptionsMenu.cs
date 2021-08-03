using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using bowen.UI;

public class OptionsMenu : Menu
{
    public static OptionsMenu instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
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
        print("Exited options menu.");
    }
}
