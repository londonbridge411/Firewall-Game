using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseCamera : MonoBehaviour
{

    public static PauseCamera instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }
}
