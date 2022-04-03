using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using bowen.AI;
using bowen.StateMachine;

public class AdwareBossMain : AI
{
    private AiStats stats;

    // Start is called before the first frame update
    void Start()
    {
        stats = GetComponent<AiStats>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public override void Attack()
    {
        throw new System.NotImplementedException();
    }

    public override void Follow()
    {
        throw new System.NotImplementedException();
    }
}

