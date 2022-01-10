using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RisingDoorOpen : MonoBehaviour, IDoor
{
    private Animator anim;
    [SerializeField] bool isOpen;

    public void Close()
    {
        anim.SetBool("isOpen", false);
        isOpen = false;
    }

    public void Open()
    {
        anim.SetBool("isOpen", true);
        isOpen = true;
    }

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponentInParent<Animator>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag.Equals("Player"))
            Open();
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag.Equals("Player"))
            Close();
    }

    public void Lock()
    {
        throw new System.NotImplementedException();
    }

    public void Unlock()
    {
        throw new System.NotImplementedException();
    }
}
