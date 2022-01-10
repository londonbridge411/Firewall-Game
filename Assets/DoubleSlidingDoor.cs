using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoubleSlidingDoor : MonoBehaviour, IDoor
{

    private Animator anim;
    [SerializeField] bool isOpen;
    public bool isLocked;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>(); 
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!isOpen && !isLocked)
            Open();
    }

    private void OnTriggerExit(Collider other)
    {
        if (isOpen)
            Close();
    }

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

    public void Lock()
    {
        if (!isLocked)
            isLocked = true;
    }

    public void Unlock()
    {
        if (isLocked)
            isLocked = false;
    }
}
