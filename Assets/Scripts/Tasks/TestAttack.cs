using System.Collections;
using System.Collections.Generic;
using UnityEngine.Animations;
using UnityEngine;

namespace bowen.AI
{
    public class TestAttack : Attack
    {
        //swing

        public override void AttackAction()
        {
            //Debug.Log(status.ToString());
        }

        public override void Execute()
        {
            //Debug.Log(status.ToString());
        }

        public override void OnEnter()
        {
            print("Started Task");
        }

        public override void OnExit()
        {
            print("Finished Task");
        }
    }
}
