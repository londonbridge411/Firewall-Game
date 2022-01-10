using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace bowen.StateMachine
{
    public abstract class State : MonoBehaviour
    {
        protected string name;
        //Entry and Exit
        public abstract void OnEnter();
        public abstract void Execute(); //Works as update function for state
        public abstract void OnExit();

        //Changers
        public abstract void SubStateChanger();
    }
}
