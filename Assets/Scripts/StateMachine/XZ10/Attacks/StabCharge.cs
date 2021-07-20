using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using bowen.AI;
using bowen.StateMachine;
using bowen.StateMachine.XZ10;

namespace bowen.AI.XZ10
{
    public class StabCharge : Attack
    {
        private BattleState battleState;
        public AnimationClip animation;

        public override void AttackAction()
        {
            throw new System.NotImplementedException();
        }

        public override void Execute()
        {
            if (Input.GetKeyDown(KeyCode.Alpha8))
            {
                battleState.ToSubState(BattleState.subState.Hold);
            }
        }
        private void Start()
        {
            battleState = (BattleState)XZ10Script.instance.FSM.currentState;
        }
        
        public override void OnEnter()
        {
            battleState = (BattleState)XZ10Script.instance.FSM.currentState;
        }

        public override void OnExit()
        {
            //battleState.anim.SetBool("Charging", false);
            //battleState.anim.SetBool("Holding", true);

            //battleState.taskManager.ChangeTask(battleState.tasks[1]);
            //battleState.ToSubState(BattleState.subState.Hold);
            print("IM GAY");
            
        }
    }
}
