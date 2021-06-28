using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using bowen.Saving;

public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        SaveLoadSystem.instance.Load();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
