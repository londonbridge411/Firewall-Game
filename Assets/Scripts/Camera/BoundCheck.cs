using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class BoundCheck : MonoBehaviour
{
    CinemachineConfiner confiner;
    bool isBounding;
    float timer = 5;
    float newtimer;


    // Start is called before the first frame update
    void Start()
    {
        confiner = CameraScript.instance.GetComponent<CinemachineConfiner>();
        newtimer = timer;
    }

    // Update is called once per frame
    void Update()
    {
        if (isBounding)
        {
            if (timer > 0)
            {
                confiner.m_Damping--;
                timer -= Time.deltaTime;
            }
            else
            {
                timer = newtimer;
                confiner.m_Damping = 10;
                isBounding = false;
            }
            //StartCoroutine(Transition(0.1f));
        }

        /*else if (confiner.m_Damping != 10f)
        {
            StartCoroutine(Transition(10f));
        }*/

        Raycasts();

    }

    private void Raycasts()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, 10);
        int i = 0;

        if (!isBounding)
        {
            foreach (Collider col in colliders)
            {
                if (col.tag.Equals("Boundary"))
                {
                    i++;
                }
                if (i > 0)
                    isBounding = true;
            }
        }

    }

    public IEnumerator Transition(float b)
    {
        for (float t = 0; t < 1; t += Time.deltaTime / 1f)
        {
            confiner.m_Damping = Mathf.Lerp(confiner.m_Damping, b, t);
            yield return null;
        }
        isBounding = false;
    }
}
