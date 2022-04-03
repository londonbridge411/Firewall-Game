using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class XZ10SwordScript : MonoBehaviour
{
    [SerializeField] private float damage;

    public void SetDamage(float dmg)
    {
        damage = dmg;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag.Equals("Player"))
        {
            PlayerStats.instance.Damage(damage, PlayerStats.ShakeAmount.LARGE);
        }
    }
}
