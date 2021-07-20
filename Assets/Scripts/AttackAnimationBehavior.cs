using System.Collections;
using System.Collections.Generic;
using bowen.StateMachine;
using UnityEngine;

namespace bowen.StateMachine.XZ10
{
    public class AttackAnimationBehavior : StateMachineBehaviour
    {
        // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
        override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
        }

        // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
        override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {

        }

        // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
        override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            BattleState.instance.ToSubState(BattleState.subState.Strafe);
            BattleState.instance.isAttacking = false;
            BattleState.instance.StartCoroutine(BattleState.instance.AttackCooldown());
        }
    }
}
