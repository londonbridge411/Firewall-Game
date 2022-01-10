using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class ElevatorDoor : MonoBehaviour, IDoor
{
    private Animator anim;
    private Elevator elevator;
    public bool isOpen;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();

        if (GetComponentInParent<Elevator>())
            elevator = GetComponentInParent<Elevator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider col)
    {
        if (!elevator.isTraveling)
        {
            if (col.tag.Equals("Player"))
            {
                Open();
            }
        }
    }

    private void OnTriggerExit(Collider col)
    {
        if (!elevator.isTraveling)
        {
            if (col.tag.Equals("Player"))
            {
                Close();
            }
        }
    }

    public void Open()
    {
        anim.SetBool("isOpen", true);
    }

    public void Close()
    {
        anim.SetBool("isOpen", false);
    }

    public void IsOpen(int open)
    {
        isOpen = (open > 0) ? true : false;
    }
    public bool AnimFinished(string stateName) => (anim.GetCurrentAnimatorStateInfo(0).normalizedTime > 1) ? true : false;

    public void Lock()
    {
        throw new System.NotImplementedException();
    }

    public void Unlock()
    {
        throw new System.NotImplementedException();
    }
}
