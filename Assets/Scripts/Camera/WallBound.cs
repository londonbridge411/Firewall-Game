using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Cinemachine;
using UnityEngine;

public class WallBound : MonoBehaviour
{
    CustomConfiner confiner;
    bool transitioning;

    private void Start()
    {
        confiner = CameraScript.instance.GetComponent<CustomConfiner>();
    }

    private void Update()
    {

    }

    public void SetDamping(float f)
    {
        confiner.m_Damping = f; //10 for smooth, 0.1 for close to wall
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag.Equals("Player"))
        {
            confiner.m_BoundingVolume = GetComponent<Collider>();

            /*if (other.tag.Equals("Player") && isTransition)
            {
                StartCoroutine(Transition(10f));
            }

            if (other.tag.Equals("Player") && !isTransition)
            {
                StartCoroutine(Transition(0.1f));
            }*/
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag.Equals("Player"))
        {
            if (confiner != this)
            {
                confiner.m_BoundingVolume = GetComponent<Collider>();
            }
        }
    }

    public IEnumerator Transition(float b)
    {
        transitioning = true;
        for (float t = 0; t < 1; t += Time.deltaTime / 1f)
        {
            SetDamping(Mathf.Lerp(confiner.m_Damping, b, t));
            yield return null;
        }
        transitioning = false;
    }

    private void OnTriggerExit(Collider other)
    {
        /*if (other.tag.Equals("Player") && isTransition)
        {
            StartCoroutine(Transition(0.1f, 1));
        }

        if (other.tag.Equals("Player") && !isTransition)
        {
            StartCoroutine(Transition(10f, 5));
        }*/
    }
}
