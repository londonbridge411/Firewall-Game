using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;
using bowen.StateMachine;

public class DashState : MonoBehaviour, IState
{
    #region Singleton
    public static DashState instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }
    #endregion

    string name = "Dash";
    Animator anim;
    [SerializeField]bool choice;

    private void Start()
    {

        anim = XZ10Script.instance.GetComponent<Animator>();
    }

    public void Execute()
    {
        if (choice)
        {
            StartCoroutine(Evade("Dash1"));
        }
        else
        {
            StartCoroutine(Evade("Dash2"));
        }
   
    }

    public string GetName() => name;

    public void OnEnter()
    {
        anim.SetBool("STATE_dash", true);
        //Choose random direction/animation
        print("Entering " + name);
        choice = (Random.value > 0.5f);

    }

    public void OnExit()
    {
        anim.SetBool("STATE_dash", false);
    }

    public void SubStateChanger()
    {
        throw new System.NotImplementedException();
    }

    public GameObject GetGameObject()
    {
        throw new System.NotImplementedException();
    }

    bool AnimatorIsPlaying()
    {
        return anim.GetCurrentAnimatorStateInfo(0).length > anim.GetCurrentAnimatorStateInfo(0).normalizedTime;
    }

    bool AnimatorIsPlaying(string stateName)
    {
        return AnimatorIsPlaying() && anim.GetCurrentAnimatorStateInfo(0).IsName(stateName);
    }

    IEnumerator Evade(string name)
    {
        anim.SetBool(name, true);
        yield return new WaitForSeconds(anim.GetCurrentAnimatorStateInfo(0).length + anim.GetCurrentAnimatorStateInfo(0).normalizedTime);
        anim.SetBool(name, false);
        XZ10Script.instance.FSM.SwitchToState(BattleState.instance);
    }
}
