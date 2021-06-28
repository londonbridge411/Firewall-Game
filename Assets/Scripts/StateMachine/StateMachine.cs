using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace bowen.StateMachine
{
    public class StateMachine : MonoBehaviour //extends to HFSM
    {
        IState currentState;
        IState previousState;

        [SerializeField] string State = string.Empty;

        private void Start()
        {

        }

        #region Getters and Setters
        public string GetName() => currentState.GetName();

        private void Update()
        {
            State = GetName();           
        }

        public void DefaultState(IState state)
        {
            currentState = state;
        }


        public void PrintState()
        {
            print(currentState.GetName());
        }

        void PrintPreviousState()
        {
            print(previousState.GetName());
        }
        #endregion

        public void StateUpdate()
        {
            if (currentState != null)
            {
                currentState.Execute();
            }
        }

        public void SwitchToState(IState state)
        {
            if (currentState == state)
            {
                throw new System.Exception("Already in state!!");
            }

            if (currentState != null)
            {
                previousState = currentState;

                currentState.OnExit();             
            }

            currentState = state;

            currentState.OnEnter();
        }
    }
}
