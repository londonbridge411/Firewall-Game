using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RisingDoor : MonoBehaviour
{
    private Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag.Equals("Player"))
            anim.SetBool("isNear", true);
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag.Equals("Player"))
            anim.SetBool("isNear", false);
    }
}
