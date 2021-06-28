using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace bowen.StateMachine
{
    public interface IState
    {
        //Entry and Exit
        void OnEnter();
        void Execute(); //Works as update function for state
        void OnExit();

        //Changers
        void SubStateChanger();

        //Getters
        string GetName();
        GameObject GetGameObject();
    }
}
