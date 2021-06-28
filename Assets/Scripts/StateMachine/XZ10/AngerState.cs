using System.Collections;
using System.Collections.Generic;
using bowen.StateMachine;
using UnityEngine;

public class AngerState : MonoBehaviour, IState
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
    public void Execute()
    {
        throw new System.NotImplementedException();
    }

    public GameObject GetGameObject() => gameObject;

    public string GetName() => name;

    public void OnEnter()
    {
        //PlayAnimation
        //Enable phase 2 enemies
        enemies.SetActive(true);

    }

    public void OnExit()
    {
        throw new System.NotImplementedException();
    }

    public void SubStateChanger()
    {
        throw new System.NotImplementedException();
    }
}
