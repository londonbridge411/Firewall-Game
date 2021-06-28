using System.Collections;
using System.Collections.Generic;
using UnityEngine.Animations;
using UnityEngine;

public class SwordScript : MonoBehaviour
{
    Animator anim;
    public bool canDamage;
    public float health;
    public float damage;
    public float critdamage;
    public float critchance;
    public float cooldownTime;
    float previousCooldown;

    private void Start()
    {
        anim = GetComponent<Animator>();
        canDamage = true;
        previousCooldown = cooldownTime;
    }
    private void Update()
    {
        StabAnim();

        if (GetComponentInParent<PlayerStats>().stamina < 0)
        {
            GetComponentInParent<PlayerStats>().stamina = 0;
        }

        //Cooldown
        if (canDamage == false)
        {
            cooldownTime -= Time.deltaTime;
            if (cooldownTime <= 0)
            {
                canDamage = true;
                cooldownTime = previousCooldown;
            }
        }
    }
    void StabAnim()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            anim.SetBool("Charge", true);
            anim.SetBool("Attack", false);
        }

        if (Input.GetButtonUp("Fire1"))
        {
            anim.SetBool("Charge", false);
            anim.SetBool("Attack", true);
            if (anim.GetBool("Attack") == true)
            {
                GetComponentInParent<PlayerStats>().stamina -= 10;
            }
        }
    }

    private void OnTriggerEnter(Collider col)
    {
        if (canDamage == true && GetComponentInParent<PlayerStats>().stamina > 0)
        {
            if (col.tag == "Hitbox")
            {
                GameObject enemy = col.gameObject;
                Attack(enemy);
            }
        }
    }

    void Attack(GameObject obj)
    {
        if (anim.GetBool("Attack") == true && canDamage == true)
        {
            obj.GetComponent<AiStats>().TakeDamage(DamageRandomizer());
            canDamage = false;
        }
    }

    public float DamageRandomizer()
    {
        float chance = critchance; //the lowest number the roll can be to crit. 100-int=actualchance
        float roll = Random.Range(0, 100); //(1, 1) for 100%
        print(roll);
        if (roll >= 100 - chance)
        {
            return critdamage;
        }
        else
            return damage;
    }
}
