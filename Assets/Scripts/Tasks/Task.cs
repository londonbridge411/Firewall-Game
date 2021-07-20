using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Task : MonoBehaviour
{
    public enum Status
    {
        STARTING, RUNNING, COMPLETED
    }

    public Status status;

    abstract public void OnEnter();
    abstract public void Execute();
    abstract public void OnExit();
}

