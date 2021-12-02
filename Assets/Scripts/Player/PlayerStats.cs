using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public static PlayerStats instance;
    
    //Player Stats
    public float health;
    public float stamina;
    public float abilityAmount;
    public float iFrameTime;
    [SerializeField]    
    private bool canDamage = true;
    
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    private void Update()
    {
        if (abilityAmount < 0)
            abilityAmount = 0;
        else if (abilityAmount > 100)
            abilityAmount = 100;
    }

    public void Damage(float dmg)
    {
        if (canDamage)
        {
            if (GameManager.overclocked)
                health -= dmg * 1.5f;   
            else
                health -= dmg;
            StartCoroutine(DamageCooldown());
        }      
    }

    public IEnumerator IFrames()
    {
        canDamage = false;
        yield return new WaitForSeconds(iFrameTime);
        canDamage = true;
    }

    private IEnumerator DamageCooldown()
    {
        canDamage = false;
        yield return new WaitForSeconds(1);
        canDamage = true;
    }

}
