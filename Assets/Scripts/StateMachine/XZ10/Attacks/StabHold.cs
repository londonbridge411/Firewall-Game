using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using bowen.AI;
using bowen.StateMachine;
using bowen.StateMachine.XZ10;

namespace bowen.AI.XZ10
{
    public class StabHold : Attack
    {
        BattleState battleState;
        private bool timerOn;
        private float prevTime;
        private float timerTime = 1.5f;

        public override void AttackAction()
        {
            throw new System.NotImplementedException();
        }

        public override void OnEnter()
        {
            timerOn = true;
            prevTime = timerTime;
            battleState = (BattleState) XZ10Script.instance.states[1];
            battleState.anim.SetBool("Holding", true);
            battleState.anim.SetBool("Charging", false);
        }

        public override void Execute()
        {
            battleState.Rotate();
            if (timerOn)
            {
                timerTime -= Time.deltaTime;

                if (timerTime <= 0)
                {
                    timerTime = prevTime;
                    timerOn = false;
                }
            }

            if (XZ10Script.instance.objectSpotted || timerOn == false)
            {
                //battleState.anim.SetBool("Charging", false);
               // battleState.ToSubState(BattleState.subState.Stab);
                // battleState.isCharging = false;
                //battleState.isAttacking = true;
                //battleState.ToSubState(StateMachine.BattleState.subState.Attack);
            }
        }

        public override void OnExit()
        {
            //Debug.Log("Exiting subState");
            //battleState.isCharging = false;
            //battleState.anim.SetBool("Charging", false);
            //if (!timerOn)
                battleState.ToSubState(BattleState.subState.Stab);
            battleState.anim.SetBool("Holding", true);
        }
    }
}
