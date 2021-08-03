using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using bowen.StateMachine;

public class XZ10Script : MonoBehaviour
{
    public StateMachine FSM;
    public NavMeshAgent nav { get; private set; }
    float timer = 1, prevTime;
    bool timerOn;
    public float meleeRange;
    public List<State> states;


    [Header("Field of View")]
    public bool objectSpotted;
    public bool bulletSpotted;
    public float minAngle = -15, maxAngle = 15;
    public LayerMask detectionLayer;

    //Stats
    public float health;
    private bool isPhase2;

    #region Singleton
    public static XZ10Script instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }
    #endregion

    private void Start()
    {
        health = GetComponent<BossStats>().health;
        prevTime = timer;
        isPhase2 = false;
        nav = GetComponentInChildren<NavMeshAgent>();
        FSM = GetComponent<StateMachine>();
        FSM.DefaultState(states[0]);
    }

    private void Update()
    {
        FOV();
        health = GetComponent<BossStats>().health;
        //Timer
        if (timerOn)
        {
            timer -= Time.deltaTime;

            if (timer <= 0)
            {
                timer = prevTime;
                timerOn = false;
            }
        }

        if (Input.GetKeyDown(KeyCode.O))
        {
            FSM.SwitchToState(states[0]); //Idle State
        }

        if (Input.GetKeyDown(KeyCode.P))
        {
            FSM.SwitchToState(states[1]); //Battle State
        }

        FSM.StateUpdate();
    }

    private void OnCollisionStay(Collision col)
    {
        if (col.gameObject.tag.Equals("Player") && timerOn == false)
        {
            PlayerStats.instance.health -= 0;
            timerOn = true;
        }
    }

    private void FOV()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, 20, detectionLayer);

        if (Vector3.Distance(transform.position, PlayerController.instance.transform.position) > 15)
        {
            objectSpotted = false;
        }

        for (int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i].transform.tag.Equals("Player"))
            {
                Vector3 targetDirection = colliders[i].transform.position - transform.position;
                float viewableAngle = Vector3.Angle(targetDirection, transform.forward);

                objectSpotted = (viewableAngle > minAngle && viewableAngle < maxAngle) ? true : false;
            }

            if (colliders[i].transform.tag.Equals("Ammo"))
            {
                Vector3 targetDirection = colliders[i].transform.position - transform.position;
                float viewableAngle = Vector3.Angle(targetDirection, transform.forward);

                bulletSpotted = (viewableAngle > minAngle && viewableAngle < maxAngle) ? true : false;
            }
        }
    }

    public void Rotate(Transform target, float turnSpeed)
    {
        Vector3 direction = (target.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * turnSpeed);
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, meleeRange);
    }

    public void SetDashFalse()
    {
        bowen.StateMachine.XZ10.DashState.instance.SetDashFalse();
    }
}
