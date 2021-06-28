using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossStats : MonoBehaviour
{
    public float health;

    private void Update()
    {
        if (health <= 0)
        {
            GetComponentInParent<ObjectManager>().DisableObject();
        }
    }

    public void TakeDamage(float dmgValue)
    {
        health -= dmgValue;
    }
}
