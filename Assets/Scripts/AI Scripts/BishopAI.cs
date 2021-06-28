using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using bowen.AI;
using UnityEngine.AI;

[RequireComponent(typeof(AiStats))]
public class BishopAI : AI
{
    private const float noticeRange = 45, attackRange = 40, fleeRange = 15;
    public Transform target;
    public Transform firePoint;
    public GameObject magicAttack;
    private Vector3 spawnPos;
    private NavMeshAgent nav;


    enum State
    {
        Idle,
        Alert,
        Follow,
        Attack,
        Flee,
    }

    //Want to have 4 colliders/triggers. Notice, Attack (Range), Flee, Attack (Melee) 
    //Also try using distance function

    AiStats stats;
    public float turnSpeed;

    //Shooting
    public float cooldownTimer;
    private float previousTime;
    [SerializeField] bool timer;

    //Melee
    public float damageCooldown;
    private float previousDMGTimer;
    [SerializeField] bool damageTimer;

    [SerializeField] State currentState;

    [SerializeField] RaycastHit hit;
    private void Start()
    {
        spawnPos = transform.position;
        stats = GetComponent<AiStats>();
        nav = GetComponent<NavMeshAgent>();
        previousTime = cooldownTimer;
        previousDMGTimer = damageCooldown;
    }

    private void Update()
    {
        float distance = Vector3.Distance(gameObject.transform.position, PlayerController.instance.transform.position);
        //Shooting
        if (timer)
        {
            cooldownTimer -= Time.deltaTime;
            if (cooldownTimer <= 0)
            {
                timer = false;
                cooldownTimer = previousTime;
            }
        }
        //Melee Damage
        if (damageTimer)
        {
            damageCooldown -= Time.deltaTime;
            if (damageCooldown <= 0)
            {
                damageTimer = false;
                damageCooldown = previousDMGTimer;
            }
        }
        //States
        switch (currentState)
        {
            case State.Idle:
                nav.SetDestination(spawnPos);
                break;
            case State.Follow:
                break;
            case State.Attack:
                if (timer == false)
                {
                    Attack();
                }
                break;
            case State.Flee:
                Flee();
                break;
            default:
                currentState = State.Idle;
                break;
        }
        if (distance <= fleeRange)
        {
            currentState = State.Flee;
        }
        else if (distance <= attackRange && distance > fleeRange)
        {
            Rotate(PlayerController.instance.transform, turnSpeed);
            if (Physics.Raycast(transform.position, transform.forward, out hit, 50f))
            {
                Debug.DrawRay(firePoint.position, transform.TransformDirection(Vector3.forward) * hit.distance, Color.green);
                if (hit.transform.tag != null && hit.transform.CompareTag("Player"))
                {
                    currentState = State.Attack;
                    nav.isStopped = true;
                }
                else
                {
                    currentState = State.Idle;
                    nav.isStopped = true;
                    //print("Oh? You're approaching me?");
                }
            }
        }
        else if (distance <= noticeRange && distance > attackRange)
        {
            if (Physics.Raycast(transform.position, transform.forward, out hit, 50f))
            {
                Rotate(PlayerController.instance.transform, turnSpeed);
                Debug.DrawRay(firePoint.position, transform.TransformDirection(Vector3.forward) * hit.distance, Color.green);
                if (hit.transform.tag != null && hit.transform.CompareTag("Player"))
                {
                    nav.isStopped = false;
                    nav.SetDestination(PlayerController.instance.transform.position);
                }
                else
                {
                    //print("Oh? You're not approaching me?");
                }
            }
        }
        else if (distance > noticeRange)
        {
            currentState = State.Idle;
        }
    }

    public override void Attack()
    {
        Instantiate(magicAttack, firePoint.position, firePoint.rotation);
        timer = true;
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player") && damageTimer == false)
        {
            PlayerStats.instance.Damage(DamageRandomizer(stats.Damage, stats.Critdamage, stats.Critchance));
            damageTimer = true;
        }
    }

    public override void Follow()
    {
        throw new System.NotImplementedException();
    }

    public void Flee()
    {
        Vector3 runTo = transform.position + ((transform.position - PlayerController.instance.transform.position)*1);
        float distance = Vector3.Distance(gameObject.transform.position, PlayerController.instance.transform.position);
        nav.isStopped = false;
        if (distance < fleeRange+2)
            nav.SetDestination(runTo);
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(gameObject.transform.position, fleeRange);
        Gizmos.DrawWireSphere(gameObject.transform.position, attackRange);
        Gizmos.DrawWireSphere(gameObject.transform.position, noticeRange);
    }
}

