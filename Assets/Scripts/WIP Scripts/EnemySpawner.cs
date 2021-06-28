using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject obj;
    public bool canSpawn = true;

    private void Start()
    {
        if (canSpawn)
        {
            Instantiate(obj, transform.position, transform.rotation);
        }     
    }

    private void Update()
    {

    }

    private void OnDrawGizmos()
    {
        
    }
}
