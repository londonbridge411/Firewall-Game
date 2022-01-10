using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Animations;

namespace bowen.StateMachine.XZ10
{
    public class BattleState : State
    {

        #region Singleton
        private void Awake()
        {
            if (instance == null)
            {
                instance = this;
            }
        }
        #endregion
        #region SubStates
        public enum subState
        {
            //Attacks
            Charge, Stab, SwingRight, SwingLeft, Strafe, Hold
        }
        #endregion

        public static BattleState instance;


        [SerializeField]
        private subState currentSubState;
        public Animator anim;
        public bool isAttacking, canAttack, isAngered;
        XZ10Script ai;
        XZ10SwordScript swordScript;
        public ParticleSystem particles;

        private void Start()
        {
            name = "Battle";
            //gameObject.SetActive(false);
            ai = XZ10Script.instance;
            anim = ai.GetComponent<Animator>();
            particles.gameObject.SetActive(false);
            swordScript = XZ10Script.instance.GetComponentInChildren<XZ10SwordScript>();
            currentSubState = subState.Strafe;
        }

        public override void OnEnter()
        {
            anim.SetBool("STATE_battle", true);
            print("Entering " + name);
            canAttack = true;
        }

        public override void OnExit()
        {
            anim.SetBool("STATE_battle", false);
            print("Exiting " + name);
            canAttack = false;
        }

        public override void Execute()
        {
            if (XZ10Script.instance.health <= 1500 && isAngered == false)
            {
                isAngered = true;
                ai.nav.speed = 30f;
            }

            //Swing trail toggler
            if (isAngered)
            {
                particles.gameObject.SetActive(true);
            }

            if (ai.bulletSpotted && currentSubState == subState.Strafe && isAttacking == false && canAttack == true)
            {
                print("DODGE GOHAN");
                XZ10Script.instance.FSM.SwitchToState(XZ10Script.instance.states[2]);
            }
            
            if (ai.objectSpotted == false && isAttacking == false)
            {
                ai.Rotate(PlayerController.instance.transform, 10);
            }

            if (isAttacking)
            {
                XZ10Script.instance.nav.velocity = Vector3.zero;
            }

            SubStateChanger();

            switch (currentSubState)
            {
                case subState.Strafe:
                    PlayStrafe();
                    break;
                case subState.Charge:
                    anim.SetBool("Charging", true);
                    canAttack = false;
                    break;
                case subState.Hold:
                    anim.SetBool("Charging", false);
                    anim.SetBool("Holding", true);
                    canAttack = false;
                    ai.Rotate(PlayerController.instance.transform, 5);
                    break;
                case subState.Stab:
                    PlayStab();
                    break;
                case subState.SwingRight:
                    PlaySwingRight();
                    break;
                case subState.SwingLeft:
                    PlaySwingLeft();
                    break;
                default:
                    break;
            }
        }

        public override void SubStateChanger()
        {
            float distance = Vector3.Distance(XZ10Script.instance.transform.position, PlayerController.instance.transform.position);
            
            //If close enough
            if (distance < 25)
            {
                if (!isAttacking && ai.objectSpotted && canAttack && !DashState.instance.isDashing)
                {
                    //Choose random attack
                    int randomAttack = Random.Range(1, 4);
                    switch (randomAttack)
                    {
                        case 1:
                            isAttacking = true;
                            ToSubState(subState.Charge);
                            break;
                        case 2:
                            isAttacking = true;
                            ToSubState(subState.SwingRight);
                            break;
                        case 3:
                            isAttacking = true;
                            ToSubState(subState.SwingLeft);
                            break;
                    }
                }
            }
        }

        #region Phase 1 Attacks
        void PlayStrafe()
        {
            anim.SetBool("Charging", false);
            anim.SetBool("Holding", false);
            anim.SetBool("isStabbing", false);
            anim.SetBool("isSwinging", false);
            anim.SetBool("isSwinging2", false);
            ai.Rotate(PlayerController.instance.transform, 10);
            ai.nav.SetDestination(PlayerController.instance.transform.position);
            isAttacking = false;
        }

        void PlayStab()
        {
            swordScript.SetDamage(40f);
            anim.SetBool("isStabbing", true);
            canAttack = false;
        }

        void PlaySwingRight()
        {
            //Swings right
            swordScript.SetDamage(15f);
            anim.SetBool("isSwinging", true);
            canAttack = false;
        }

        void PlaySwingLeft()
        {
            //Swings left
            swordScript.SetDamage(15f);
            anim.SetBool("isSwinging2", true);
            canAttack = false;
        }
        #endregion

        #region Phase 2 Attacks
        void Slam()
        {
            swordScript.SetDamage(15f);
            anim.SetBool("isSlamming", true);
            canAttack = false;
            StartCoroutine(AttackCooldown());
            ToSubState(subState.Strafe);
        }

        void Flurry()
        {
            swordScript.SetDamage(15f);
            anim.SetBool("isFlurry", true);
            canAttack = false;
            StartCoroutine(AttackCooldown());
            ToSubState(subState.Strafe);
        }
        #endregion

        public void ToSubState(subState state)
        {
            currentSubState = state;
        }

        public IEnumerator AttackCooldown()
        {
            canAttack = false;
            yield return new WaitForSeconds(1.25f);
            canAttack = true;
        }

        public void Rotate()
        {
            ai.Rotate(PlayerController.instance.transform, 5);
        }
    }

    public class Battle_Strafe : State
    {
        public override void Execute()
        {
            throw new System.NotImplementedException();
        }

        public override void OnEnter()
        {
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

    public class Battle_Swing : State
    {
        public override void Execute()
        {
            throw new System.NotImplementedException();
        }

        public override void OnEnter()
        {
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
}
