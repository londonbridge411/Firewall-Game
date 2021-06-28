using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDrop : MonoBehaviour
{
    public GameObject item;
    public float itemDropChance;

    public void Drop()
    {
        float roll = Random.Range(0, 100); //(1, 1) for 100%
        if (roll >= (100 - itemDropChance))
        {
            Instantiate(item, transform.position, transform.rotation);
        }
    }
}
