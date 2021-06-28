using System.Collections;
using System.Collections.Generic;
using bowen.StateMachine;
using UnityEngine.AI;
using UnityEngine;

public class RangedState : MonoBehaviour, IState
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

    string name = "Ranged";

    [SerializeField]
    private subState currentSubState;

    public Animator anim;
    public bool isCharging, isAttacking, canAttack;
    private NavMeshAgent nav;
    XZ10Script ai;
    XZ10SwordScript swordScript;

    public void Execute()
    {
        throw new System.NotImplementedException();
    }

    public void OnEnter()
    {
        throw new System.NotImplementedException();
    }

    public void OnExit()
    {
        throw new System.NotImplementedException();
    }

    public void SubStateChanger()
    {
        throw new System.NotImplementedException();
    }

    #region Unimplemented
    public GameObject GetGameObject()
    {
        throw new System.NotImplementedException();
    }

    public string GetName()
    {
        throw new System.NotImplementedException();
    }
    #endregion
}
