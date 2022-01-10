using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace bowen.StateMachine
{
    public class IdleState : State
    {
        public override void Execute()
        {
            //Does nothing
        }

        public override void SubStateChanger()
        {
            throw new System.NotImplementedException();
        }

        public override void OnEnter()
        {
            name = "Idle";
            print("Entering " + name);
        }

        public override void OnExit()
        {
            print("Exiting " + name);
        }
    }
}