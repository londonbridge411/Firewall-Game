using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntranceDoor : MonoBehaviour, IDoor
{

    Animator anim;
    public bool isOpen;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isOpen)
            Open();
    }

    public void Close()
    {
        throw new System.NotImplementedException();
    }

    public void Open()
    {
        anim.SetBool("isOpen", true);
    }
}