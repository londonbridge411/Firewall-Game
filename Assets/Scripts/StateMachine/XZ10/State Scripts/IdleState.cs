using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace bowen.StateMachine
{
    public class IdleState : State
    {
        string name = "Idle";

        public override void Execute()
        {
            //Does nothing
        }

        public override string GetName() => name;

        public override GameObject GetGameObject() => gameObject;


        public override void SubStateChanger()
        {
            throw new System.NotImplementedException();
        }

        public override void OnEnter()
        {
            print("Entering " + name);
        }

        public override void OnExit()
        {
            print("Exiting " + name);
        }
    }
}