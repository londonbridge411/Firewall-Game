using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSong : MonoBehaviour
{
    public string name;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider col)
    {
        if (col.tag.Equals("Player"))
        {
            if (!AudioManager.instance.backgroundMusic.Equals(name))
            {
                AudioManager.instance.SwapSong(name);
            }
        }
    }
}
