using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityTimeStop : Ability
{
    public override void Activate()
    {
        GameManager.stoppedTime = true;

        Object[] list = Resources.FindObjectsOfTypeAll(typeof(Rigidbody));

        foreach (Rigidbody rb in list)
        {
            rb.isKinematic = true;
        }

        PlayerController.instance.GetComponent<Rigidbody>().isKinematic = false;

        GameManager.instance.StoppedTime();
    }

    public override void Deactivate()
    {
        GameManager.instance.ResumedTime();
        
        Object[] list = Resources.FindObjectsOfTypeAll(typeof(Rigidbody));

        foreach (Rigidbody rb in list)
        {
            rb.isKinematic = false;
        }
    }
}
