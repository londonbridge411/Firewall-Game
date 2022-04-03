using System.Collections;
using System.Collections.Generic;
using UnityEngine.Animations;
using UnityEngine;

public class SwordScript : MonoBehaviour
{
    public enum SwordState
    {
        NOT_IN_USE,
        IDLE,
        SLICE,
        SLICE2,
        BLOCK,
        PARRY
    }

    public SwordState state = SwordState.NOT_IN_USE;
    private SwordAnimEvents swordAnim;
    public bool canDamage;
    public float health;
    public float damage;
    public float critdamage;
    public float critchance;
    public float cooldownTime;
    public float blockCost;
    public ParticleSystem blockEffect;

    float previousCooldown;

    private void Start()
    {
        swordAnim = GetComponentInParent<SwordAnimEvents>();
        canDamage = true;
        previousCooldown = cooldownTime;
    }

    private void Update()
    {
        if (Input.GetButtonDown("Fire1")) //Swing
        {
            if (state == SwordState.BLOCK)
            {
                swordAnim.SwordAttack(SwordState.PARRY);
            }
            else
            {
                int attack = Random.Range(0, 2);
                print("ATTACK " + attack);
                if (attack == 0)
                    SliceR();
                else
                    SliceL();
            }
        }
        else if (Input.GetButton("Fire2")) //Blocking State
        {
            PlayerController.instance.moveSpeed = 5f;
            if (state == SwordState.IDLE)
                Block();
        }
        else if (state == SwordState.BLOCK) //For exiting blocking state
        {
            swordAnim.anim.SetBool("Block", false);
            state = SwordState.IDLE;
        }

    }

    private void SliceR()
    {
        swordAnim.SwordAttack(SwordState.SLICE);
    }

    private void SliceL()
    {
        swordAnim.SwordAttack(SwordState.SLICE2);
    }

    private void Block()
    {
        swordAnim.SwordAttack(SwordState.BLOCK);
    }

    private void OnTriggerEnter(Collider other)
    {
        

        // Blocking
        if (state == SwordState.BLOCK && other.GetComponent<LethalObject>())
        {
            // Anim change
            swordAnim.anim.SetTrigger("Blocked");

            float predictedStamina = PlayerController.instance.stamina_Bar.MAX_STAMINA - blockCost;
            if (predictedStamina > 20)
            {
                // Stamina Loss
                PlayerController.instance.stamina_Bar.MAX_STAMINA -= blockCost;
                PlayerStats.instance.LoseStamina(blockCost);

                // Effects
                StartCoroutine(CameraScript.instance.Shake(2, 2, .5f));
                StartCoroutine(PlayerController.instance.Knockback(1f, 500f, other.transform));
            }
            else
            {
                // Health Loss
                LethalObject obj = other.GetComponent<LethalObject>();
                //StartCoroutine(CameraScript.instance.Shake(5, 2, .5f));
                obj.DamagePlayer(obj.damage / 1.5f, 1.5f, 1250, PlayerStats.ShakeAmount.LARGE);
                //StartCoroutine(PlayerController.instance.Knockback(1f, 500f, other.transform));
            }

            // Despawn
            blockEffect.Play();
            ObjectPooler.instance.Despawn(other.gameObject);
        }
    }
    /*private void SliceR()
    {
        state = SwordState.SLICE;
        anim.SetBool("SwingR", true);
        yield return new WaitForSeconds(anim.GetCurrentAnimatorStateInfo(0).length + anim.GetCurrentAnimatorStateInfo(0).normalizedTime);
        anim.SetBool("SwingR", false);
    }*/
}

#region Old Code
/*private void Update()
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
}*/
#endregion
