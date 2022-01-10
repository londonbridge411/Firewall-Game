using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialDash : MonoBehaviour
{
    private void Update()
    {
        if (Input.GetButton("Dash"))
        {
            Time.timeScale = 1f;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag.Equals("Player"))
        {
            print("KCHAOW");
            Time.timeScale = 0.1f;
        }
    }
}
