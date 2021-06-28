using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class ForceBound : MonoBehaviour
{
    CustomConfiner confiner;
    public BoxCollider bound;

    private void Start()
    {
        confiner = CameraScript.instance.GetComponent<CustomConfiner>();
    }

    private void OnTriggerStay(Collider col)
    {
        if (col.tag.Equals("Player"))
        {
            confiner.m_BoundingVolume = bound;
        }
    }

    /*private void OnTriggerExit(Collider col)
    {
        if (col.tag.Equals("Player"))
        {
            if (bound.GetComponent<WallBound>().isTransition)
                StartCoroutine(bound.GetComponent<WallBound>().Transition(10f));
            else
                StartCoroutine(bound.GetComponent<WallBound>().Transition(0.1f));
        }
    }*/     

}
