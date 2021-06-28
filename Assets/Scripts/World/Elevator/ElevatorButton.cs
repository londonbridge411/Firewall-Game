using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElevatorButton : MonoBehaviour
{
    private Elevator elevator;
    private Animator anim;
    public bool isOn;

    private void Start()
    {
        elevator = GetComponentInParent<Elevator>();
        anim = GetComponent<Animator>();
    }

    private void OnTriggerEnter(Collider col)
    {
        if (col.tag.Equals("Player"))
        {
            elevator.Move();
            anim.SetBool("isOn", true);
            isOn = true;
        }

    }

    public void TurnOff(Collider col)
    {
        if (col.tag.Equals("Player"))
        {
            anim.SetBool("isOn", false);
            isOn = false;
        }
    }
}
