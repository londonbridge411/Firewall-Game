using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Animations;

namespace bowen.StateMachine
{
    public class BattleState : MonoBehaviour, IState
    {
        
        #region SubStates
        public enum subState
        {
            //Attacks
            Charging, Attack, Attack2, Attack3, Strafe
        }
        #endregion

        #region Singleton
        public static BattleState instance;

        private void Awake()
        {
            if (instance == null)
            {
                instance = this;
            }
        }
        #endregion

        string name = "Battle";

        [SerializeField]
        private subState currentSubState;

        public Animator anim;
        public bool isCharging, isAttacking, canAttack, isAngered;
        XZ10Script ai;
        XZ10SwordScript swordScript;
        public ParticleSystem particles;

        private void Start()
        {
            //gameObject.SetActive(false);
            ai = XZ10Script.instance;
            anim = ai.GetComponent<Animator>();
            particles.gameObject.SetActive(false);
            swordScript = XZ10Script.instance.GetComponentInChildren<XZ10SwordScript>();
            currentSubState = subState.Strafe;
        }

        public GameObject GetGameObject() => gameObject;

        public string GetName() => name;


        public void OnEnter()
        {
            anim.SetBool("STATE_battle", true);
            print("Entering " + name);
            canAttack = true;
        }

        public void OnExit()
        {
            anim.SetBool("STATE_battle", false);
            print("Exiting " + name);
            canAttack = false;
        }

        public void Execute()
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

            if (ai.bulletSpotted && currentSubState == subState.Strafe && isAttacking == false && canAttack == true && isCharging == false)
            {
                print("DODGE GOHAN");
                XZ10Script.instance.FSM.SwitchToState(DashState.instance);
            }
            
            if (ai.objectSpotted == false && isAttacking == false)
            {
                ai.Rotate(PlayerController.instance.transform, 10);
            }
            SubStateChanger();

            switch (currentSubState)
            {
                case subState.Strafe:
                    if (isAttacking == false)
                    {
                        ai.Rotate(PlayerController.instance.transform, 10);
                        ai.nav.SetDestination(PlayerController.instance.transform.position);
                    }
                    break;
                case subState.Charging:
                    ai.Rotate(PlayerController.instance.transform, 5);
                    if (isCharging == true)
                    {
                        anim.SetBool("Charging", true);
                    }
                    else
                        anim.SetBool("Charging", false);
                    break;
                case subState.Attack:
                    isCharging = false;
                    Stab();
                    break;
                case subState.Attack2:
                    Swing1();
                    break;
                case subState.Attack3:
                    Swing2();
                    break;
                default:
                    break;
            }
        }

        public void SubStateChanger()
        {
            float distance = Vector3.Distance(XZ10Script.instance.transform.position, PlayerController.instance.transform.position);
            
            //If close enough
            if (distance < 25)
            {
                if (isAttacking == false && ai.objectSpotted)
                {
                    //Choose random attack
                    int randomAttack = Random.Range(1, 4);
                    switch (randomAttack)
                    {
                        case 1:
                            isAttacking = true;
                            isCharging = true;
                            ToSubState(subState.Charging);
                            break;
                        case 2:
                            isAttacking = true;
                            //ai.Rotate(PlayerController.instance.transform, 5);
                            ToSubState(subState.Attack2);
                            break;
                        case 3:
                            isAttacking = true;
                            ToSubState(subState.Attack3);
                            break;
                    }
                }
            }
        }

        #region Phase 1 Attacks
        void Stab()
        {
            swordScript.SetDamage(40f);
            anim.SetBool("isStabbing", true);
            canAttack = false;
            StartCoroutine(AttackCooldown());
            ToSubState(subState.Strafe);
        }

        void Swing1()
        {
            //Swings right
            swordScript.SetDamage(15f);
            anim.SetBool("isSwinging", true);
            canAttack = false;
            StartCoroutine(AttackCooldown());
            ToSubState(subState.Strafe);
        }

        void Swing2()
        {
            //Swings left
            swordScript.SetDamage(15f);
            anim.SetBool("isSwinging2", true);
            canAttack = false;
            StartCoroutine(AttackCooldown());
            ToSubState(subState.Strafe);
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

        IEnumerator AttackCooldown()
        {
            yield return new WaitForSeconds(2f);
            canAttack = true;
        }
    }
}
