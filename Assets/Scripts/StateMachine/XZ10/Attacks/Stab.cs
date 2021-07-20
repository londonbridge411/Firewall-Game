using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using bowen.AI;
using bowen.StateMachine;
using bowen.StateMachine.XZ10;

namespace bowen.AI.XZ10
{
    public class Stab : Attack
    {
        public AnimationClip animation;
        BattleState battleState;

        public override void OnEnter()
        {
            battleState = (BattleState)XZ10Script.instance.states[1];
            battleState.anim.SetBool("isStabbing", true);
            battleState.anim.SetBool("Charging", false);
        }

        public override void AttackAction()
        {
            throw new System.NotImplementedException();
        }

        public override void Execute()
        {
            throw new System.NotImplementedException();
        }

        public override void OnExit()
        {
            battleState.isAttacking = false;
            battleState.anim.SetBool("isStabbing", false);
            battleState.anim.SetBool("Holding", false);
            battleState.anim.SetBool("Charging", false);
            battleState.ToSubState(BattleState.subState.Strafe);
            //battleState.taskManager.ClearTask();
        }
    }
}