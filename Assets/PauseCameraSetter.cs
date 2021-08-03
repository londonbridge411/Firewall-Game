using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseCameraSetter : MonoBehaviour
{

    Canvas canvas;


    private void Update()
    {
        if (canvas.worldCamera == null)
            canvas.worldCamera = PauseCamera.instance.GetComponent<Camera>();
    }
}
