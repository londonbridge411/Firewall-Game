using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace bowen.StateMachine
{
    public class IdleState : MonoBehaviour, IState
    {
        #region Singleton
        public static IdleState instance;

        private void Awake()
        {
            if (instance == null)
            {
                instance = this;
            }
        }
        #endregion

        string name = "Idle";

        public void Execute()
        {
            //Does nothing
        }

        public string GetName() => name;

        public GameObject GetGameObject() => gameObject;


        public void SubStateChanger()
        {
            throw new System.NotImplementedException();
        }

        public void OnEnter()
        {
            print("Entering " + name);
        }

        public void OnExit()
        {
            print("Exiting " + name);
        }
    }
}