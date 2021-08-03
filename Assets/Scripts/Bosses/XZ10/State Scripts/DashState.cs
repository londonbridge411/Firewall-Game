using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;
using bowen.StateMachine;

namespace bowen.StateMachine.XZ10
{
    public class DashState : State
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
        [SerializeField] bool choice;
        public bool isDashing;

        private void Start()
        {
            anim = XZ10Script.instance.GetComponent<Animator>();
        }

        public override void Execute()
        {
            if (choice)
                Evade("Dash1");
            else
                Evade("Dash2");

        }

        public override string GetName() => name;

        public override void OnEnter()
        {
            anim.SetBool("STATE_dash", true);
            //Choose random direction/animation
            print("Entering " + name);
            choice = (Random.value > 0.5f);

        }

        public override void OnExit()
        {
            anim.SetBool("STATE_dash", false);
        }

        public override void SubStateChanger()
        {
            throw new System.NotImplementedException();
        }

        public override GameObject GetGameObject()
        {
            throw new System.NotImplementedException();
        }

        void Evade(string name)
        {
            anim.SetBool(name, true);
            isDashing = true;
        }

        public void SetDashFalse()
        {
            anim.SetBool("Dash1", false);
            anim.SetBool("Dash2", false);
            choice = false;
            isDashing = false;
            XZ10Script.instance.FSM.SwitchToState(BattleState.instance);
        }
    }
}