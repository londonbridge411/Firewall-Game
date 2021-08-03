using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemRotator : MonoBehaviour
{
    public bool canDrag;
    public bool isInbounds;
    public bool isDragging;
    float rotSpeed = 100;

    public void Enter()
    {
        canDrag = true;
        isInbounds = true;
    }

    public void Exit()
    {
        isInbounds = false;
        if (isDragging == false)
            canDrag = false;
    }

    public void Exit2()
    {
        if (isInbounds == false)
            canDrag = false;
    }

    void Update()
    {
        if (Input.GetMouseButton(0) && canDrag)
        {
            transform.GetChild(0).Rotate(new Vector3(Input.GetAxis("Mouse Y"), Input.GetAxis("Mouse X"), 0) * Time.unscaledDeltaTime * rotSpeed);
            isDragging = true;
        }
        else
            isDragging = false;
    }
}
