using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace bowen.StateMachine
{
    public class StateMachine : MonoBehaviour
    {
        public State currentState;
        State previousState;

        [SerializeField] string State = string.Empty;

        private void Start()
        {
            currentState.Execute();
        }

        #region Getters and Setters
        public string GetName() => currentState.name;

        private void Update()
        {
            State = GetName();          
        }

        public void DefaultState(State state)
        {
            currentState = state;
        }


        public void PrintState()
        {
            print(currentState.name);
        }

        void PrintPreviousState()
        {
            print(previousState.name);
        }
        #endregion

        public void StateUpdate()
        {
            if (currentState != null)
            {
                currentState.Execute();
            }
        }

        public void SwitchToState(State state)
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
