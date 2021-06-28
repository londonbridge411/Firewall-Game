using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : MonoBehaviour
{
    private float health = 100;

    // Update is called once per frame
    void Update()
    {
        if (health <= 0)
        {
            ObjectPooler.instance.Despawn(gameObject);
        }
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
    }
}
