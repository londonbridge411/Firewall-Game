using System.Collections;
using System.Collections.Generic;
using bowen.StateMachine;
using UnityEngine;

public class AngerState : State
{
    #region Singleton
    public static AngerState instance;
    string name = "Anger";

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }
    #endregion

    public GameObject enemies;

    public override void OnEnter()
    {
        name = "Anger";
        enemies.SetActive(true);
    }

    public override void Execute()
    {
        throw new System.NotImplementedException();
    }

    public override void OnExit()
    {
        throw new System.NotImplementedException();
    }

    public override void SubStateChanger()
    {
        throw new System.NotImplementedException();
    }
}
