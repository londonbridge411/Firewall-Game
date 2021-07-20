using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TaskManager : MonoBehaviour
{
    public Task currentTask;
    public bool isBusy;

    private void Start()
    {
        if (currentTask != null)
        {
            currentTask.OnEnter();
        }
    }
    
    private void Update()
    {
        if (currentTask != null)
        {
            currentTask.Execute();
            //currentTask.status = Task.Status.RUNNING;
        }

        /*if (queue.Count == 0)
            print("empty queue");
        else
            PrintQueue();*/
        //print("Task: " + currentTask.name + " " + currentTask.status.ToString());
    }

    public void OnEnter()
    {
        if (currentTask != null)
        {
            currentTask.status = Task.Status.STARTING;
            isBusy = true;
            print("Starting new task!");
            currentTask.OnEnter();
        }

    }

    public void OnExit()
    {
        if (currentTask != null)
        {
            //currentTask.status = Task.Status.COMPLETED;
            print("Completed Task!!!!!!!!!!");
            currentTask.OnExit();
            isBusy = false;
            //ChangeTask(null);
        }
    }

    public void ChangeTask(Task newTask)
    {
        if (!isBusy && currentTask != null)
        {
            //OnExit();
        }

        if (!isBusy) //Stops initial task
        {
            OnExit();
            currentTask = newTask;
            OnEnter();
        }
    }

    public void ChangeTaskStatus(Task.Status status)
    {
        currentTask.status = status;
    }

    public void ClearTask()
    {
        currentTask = null;
        if (!isBusy) //Stops initial task
        {
            currentTask = null;
        }
    }
}
