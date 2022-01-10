using System.Collections;
using System.Collections.Generic;
using bowen.StateMachine;
using UnityEngine.AI;
using UnityEngine;

public class RangedState : State
{
    #region SubStates
    public enum subState
    {
        //Attacks
        SwordSlam, SingleMagic, MultiMagic, Strafe
    }
    #endregion

    #region Singleton
    public static RangedState instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }
    #endregion

    

    [SerializeField]
    private subState currentSubState;

    public Animator anim;
    public bool isCharging, isAttacking, canAttack;
    private NavMeshAgent nav;
    XZ10Script ai;
    XZ10SwordScript swordScript;

    public override void Execute()
    {
       
    }

    public override void OnEnter()
    {
        name = "Ranged";
        throw new System.NotImplementedException();
    }

    public override void OnExit()
    {
        throw new System.NotImplementedException();
    }

    public override void SubStateChanger()
    {
        throw new System.NotImplementedException();
    }
}
