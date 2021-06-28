using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShellDisappear : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        StartCoroutine(ObjectPooler.instance.Despawn(gameObject, 5f));
    }
}
