using System.Collections;
using System.Collections.Generic;
using bowen.StateMachine;
using UnityEngine;

public class StabHoldBehavior : StateMachineBehaviour
{
    private bool timerOn;
    private float prevTime;
    private float timerTime = 1.5f;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        timerOn = true;
        prevTime = timerTime;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
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
            BattleState.instance.anim.SetBool("Charging", false);
            BattleState.instance.isCharging = false;
            BattleState.instance.isAttacking = true;
            BattleState.instance.ToSubState(BattleState.subState.Attack);
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Debug.Log("Exiting subState");
        BattleState.instance.isCharging = false;
        BattleState.instance.anim.SetBool("Charging", false);
    }

    // OnStateMove is called right after Animator.OnAnimatorMove()
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that processes and affects root motion
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK()
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that sets up animation IK (inverse kinematics)
    //}
}
