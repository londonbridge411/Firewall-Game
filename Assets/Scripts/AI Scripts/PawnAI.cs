using bowen.AI;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
[RequireComponent(typeof(AiStats))]
public class PawnAI : AI
{
    enum State
    {
        Idle,
        Alert,
        Follow,
    }

    [SerializeField] private NavMeshAgent nav;
    private AiStats stats;
    public Transform target;
    public float radius;
    public float cooldownTimer;
    private float previousTime;
    [SerializeField] bool timer;

    [SerializeField] State currentState;
    private void Start()
    {
        nav = GetComponent<NavMeshAgent>();
        target = GameObject.FindGameObjectWithTag("Player").transform;
        stats = GetComponent<AiStats>();
        previousTime = cooldownTimer;

        GameManager.instance.OnTimeStop += OnTimeStop;
        GameManager.instance.OnTimeResume += OnTimeResume;
    }
    
    private void Update()
    {

        //Damage Timer
        if (timer)
        {
            cooldownTimer -= Time.deltaTime;
            if (cooldownTimer <= 0)
            {
                timer = false;
                cooldownTimer = previousTime;
            }
        }
        switch (currentState)
        {
            case State.Idle:
                break;
            case State.Follow:
                    Follow();
                break;
            default:
                currentState = State.Idle;
                break;
        }
    }

    private void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            currentState = State.Follow;
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Attack();
        }
    }

    #region Inherited AI Functions
    public override void Attack()
    {
        if (timer == false)
        {
            float damageDealt = DamageRandomizer(stats.Damage, stats.Critdamage, stats.Critchance);
            PlayerStats.ShakeAmount shake = (damageDealt == stats.Damage) ? PlayerStats.ShakeAmount.MEDIUM : PlayerStats.ShakeAmount.EXTRA_LARGE;
            PlayerStats.instance.Damage(damageDealt, shake);
            timer = true;
        }
    }

    public override void Follow()
    {
        nav.SetDestination(target.position);
        Collider[] colliders = Physics.OverlapSphere(transform.position, radius); //An array of colliders
        foreach (Collider col in colliders)
        {
            try
            {
                if (tag == "Enemy")
                {
                    col.GetComponent<PawnAI>().currentState = State.Follow;
                }
            }
            catch (NullReferenceException e)
            {

            }
        }
    }
    #endregion

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(gameObject.transform.position, radius);
    }

    void OnTimeStop() => nav.enabled = false;

    void OnTimeResume() => nav.enabled = true;
}
