using bowen.AI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(AiStats), typeof(NavMeshAgent))]
public class KnightAI : AI
{
    private NavMeshAgent nav;
    private AiStats stats;
    public Transform target;
    [SerializeField] private GameObject shield;
    //Ranges
    public float attackRange, rangedAtkRange, noticeRange, followRange;

    //Patrol Points
    public Transform[] patrolPoints;
    private int destPoint = 0;

    //Privates
    float halfHealth;
    float turnSpeed = 5f;
    float regTurnSpeed;
    float slowSpeed = 2.5f;
    float walkSpeed;
    float chargeSpeed = 100f;
    float distance;
    bool spotted;

    //Timer
    public float timer;
    float prevTime;
    bool timerOn;

    RaycastHit hit;

    enum State
    {
        Idle,
        Patrol,
        Following,
        Battle,
    }

    [SerializeField] State currentState;

    // Start is called before the first frame update
    void Start()
    {
        nav = gameObject.GetComponent<NavMeshAgent>();
        target = GameObject.FindGameObjectWithTag("Player").transform;
        shield = GetComponentInChildren<Shield>().gameObject;
        currentState = State.Idle;
        stats = GetComponent<AiStats>();
        Shield(false);
        regTurnSpeed = turnSpeed;
        walkSpeed = nav.speed;
        prevTime = timer;
        timerOn = false;
        StartCoroutine(HalfHealthFix());

        GameManager.instance.OnTimeStop += OnTimeStop;
        GameManager.instance.OnTimeResume += OnTimeResume;
    }

    // Update is called once per frame
    void Update()
    {
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

        distance = Vector3.Distance(transform.position, target.position);
        //Raycast stuff from Bishop

        if (shield != null && shield.activeSelf == true)
        {
            turnSpeed = slowSpeed;
        }
        else
        {
            turnSpeed = regTurnSpeed;
        }

        if (stats.Health <= halfHealth)
        {
            if (shield != null)
            {
                Shield(true);
                Rotate(target, turnSpeed);
            }
        }

        //States
        switch (currentState)
        {
            case State.Idle:
                //DO NOTHING
                break;
            case State.Patrol:
                nav.speed = regTurnSpeed;
                nav.speed = walkSpeed;
                if (!nav.pathPending && nav.remainingDistance < 0.5f)
                    Patrol();
                break;
            case State.Following:
                break;
            case State.Battle:
                //Rotation and Movement
                Rotate(target, turnSpeed);
                if (Physics.Raycast(transform.position, transform.forward, out hit, 100f))
                {
                    Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * hit.distance, Color.green);

                    if (hit.transform.tag != null && hit.transform.CompareTag("Player"))
                        nav.SetDestination(target.position);
                }

                //Attack
                if (distance <= attackRange)
                    Attack();
                break;
            default:
                break;
        }

        StateControl();

    }

    public override void Attack()
    {
        nav.speed = chargeSpeed;
    }

    public override void Follow()
    {
        throw new System.NotImplementedException();
    }

    void Shield(bool toggle)
    {
        if (shield != null)
        {
            shield.SetActive(toggle);
        }
    }

    void Patrol()
    {
        // Returns if no points have been set up
        if (patrolPoints.Length == 0)
        {
            return;
        }

        // Set the agent to go to the currently selected destination.
        nav.destination = patrolPoints[destPoint].position;

        // Choose the next point in the array as the destination,
        // cycling to the start if necessary.
        destPoint = (destPoint + 1) % patrolPoints.Length;
    }

    private void OnCollisionStay(Collision col)
    {
        if (col.collider.CompareTag("Player") && timerOn == false)
        {
            float damageDealt = DamageRandomizer(stats.Damage, stats.Critdamage, stats.Critchance);
            PlayerStats.ShakeAmount shake = (damageDealt == stats.Damage) ? PlayerStats.ShakeAmount.MEDIUM : PlayerStats.ShakeAmount.EXTRA_LARGE;
            PlayerStats.instance.Damage(damageDealt, shake);
            timerOn = true;
        }
    }

    void StateControl()
    {
        if (distance > followRange)
        {
            spotted = false;
        }
        if (distance > noticeRange && distance < followRange)
        {
            currentState = (spotted) ? State.Battle : State.Patrol;
        }
        if (distance <= noticeRange)
        {
            spotted = true;
            currentState = State.Battle;
        } 
    }

    IEnumerator HalfHealthFix()
    {
        yield return new WaitForSeconds(0.000001f);
        //halfHealth = (stats.Health / 2);
    }

    IEnumerator AttackCooldown()
    {
        yield return new WaitForSeconds(2);
        nav.speed = walkSpeed;
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(gameObject.transform.position, rangedAtkRange);
        Gizmos.DrawWireSphere(gameObject.transform.position, attackRange);
        Gizmos.DrawWireSphere(gameObject.transform.position, noticeRange);
        Gizmos.DrawWireSphere(gameObject.transform.position, followRange);
        if (patrolPoints.Length != 0)
        {
            for (int i = 0; i < patrolPoints.Length; i++)
            {
                Gizmos.DrawWireSphere(patrolPoints[i].position, 1);
            }
        }
    }

    void OnTimeStop() => nav.enabled = false;

    void OnTimeResume() => nav.enabled = true;
}
