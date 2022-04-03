using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordAnimEvents : MonoBehaviour
{

    SwordScript sword;
    public Animator anim { get; private set; }
    // Start is called before the first frame update
    void Start()
    {
        sword = GetComponentInChildren<SwordScript>();
        anim = GetComponent<Animator>();
        GetComponent<WeaponSwapper>().OnWeaponChange += EnableDisable;
    }

    // Update is called once per frame
    void EnableDisable()
    {
        if (GetComponent<WeaponSwapper>().WeaponNumber == 0)
        {
            anim.SetBool("InUse", true);
            sword.state = SwordScript.SwordState.IDLE;
        }
        else
        {
            anim.SetBool("InUse", false);
            sword.state = SwordScript.SwordState.NOT_IN_USE;
        }
    }

    public void SwordAttack(SwordScript.SwordState newState)
    {
        sword.state = newState;
        GameManager.instance.FreezeCursor();

        switch (sword.state)
        {
            case SwordScript.SwordState.SLICE:
                {
                    PlayerController.instance.canMove = false;
                    PlayerController.instance.canRotate = false;
                    anim.SetBool("SwingR", true);
                    break;
                }
            case SwordScript.SwordState.SLICE2:
                {
                    PlayerController.instance.canMove = false;
                    PlayerController.instance.canRotate = false;
                    anim.SetBool("SwingL", true);
                    break;
                }
            case SwordScript.SwordState.BLOCK:
                {
                    float speed = PlayerController.instance.moveSpeed;
                    PlayerController.instance.moveSpeed = 5f;
                    anim.SetBool("Block", true);

                    break;
                }
            case SwordScript.SwordState.PARRY:
                {
                    PlayerController.instance.canMove = false;
                    PlayerController.instance.canRotate = false;
                    anim.SetTrigger("Parry");
                    //anim.SetBool("Block", false);
                    Debug.Log("Parry!");
                    break;
                }
            case SwordScript.SwordState.NOT_IN_USE:
                {
                    throw new System.Exception("Can't use this state as an attack");
                }
        }
    }

    public void ComboSword(SwordScript.SwordState a)
    {

    }

    public void EndAttack()
    {
        //Sets all attack bools to false
        anim.SetBool("SwingR", false);
        anim.SetBool("SwingL", false);
        anim.SetBool("Block", false);
        GameManager.instance.ResumeCursor();
        PlayerController.instance.canMove = true;
        PlayerController.instance.canRotate = true;
        sword.state = SwordScript.SwordState.IDLE;
    }

    public void EnableMoveState(SwordScript.SwordState state)
    {
        switch (state)
        {
            case SwordScript.SwordState.SLICE:
                {
                    anim.SetBool("SwingR", true);
                    break;
                }
            case SwordScript.SwordState.SLICE2:
                {
                    anim.SetBool("SwingL", true);
                    break;
                }
            case SwordScript.SwordState.BLOCK:
                {
                    anim.SetBool("Block", true);
                    break;
                }
        }
    }

    public void DisableMoveState(SwordScript.SwordState state)
    {
        switch (state)
        {
            case SwordScript.SwordState.SLICE:
                {
                    anim.SetBool("SwingR", false);
                    break;
                }
            case SwordScript.SwordState.SLICE2:
                {
                    anim.SetBool("SwingL", false);
                    break;
                }
            case SwordScript.SwordState.BLOCK:
                {
                    anim.SetBool("Block", false);
                    break;
                }
        }
    }

    public void TakeStamina(float i)
    {
        PlayerStats.instance.LoseStamina(i);
    }
}
