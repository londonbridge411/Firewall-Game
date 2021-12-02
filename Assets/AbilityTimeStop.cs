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
        StartCoroutine(GameManager.StoppedTimeLerp());
        StartCoroutine(GameManager.LensEffect());
        StartCoroutine(CameraScript.instance.Shake(1.5f, 2, 1f));
    }

    public override void Deactivate()
    {
        GameManager.instance.ResumedTime();

        if (GameManager.stoppedTime)
        {
            StartCoroutine(GameManager.StoppedTimeRevert());
            GameManager.stoppedTime = false;
        }
        
        Object[] list = Resources.FindObjectsOfTypeAll(typeof(Rigidbody));

        foreach (Rigidbody rb in list)
        {
            rb.isKinematic = false;
        }
    }
}
