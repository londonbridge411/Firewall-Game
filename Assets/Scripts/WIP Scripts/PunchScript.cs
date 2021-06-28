using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PunchScript : MonoBehaviour
{
    private Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Melee"))
        {
            //var i = WeaponSwapper.instance.weapons.IndexOf(gameObject);
            //WeaponSwapper.instance.Swap(i);
            //Punch();
        }
    }

    public void Punch()
    {
        int roll = Random.Range(0, 2); //(1, 1) for 100%
        print(roll);
        if (roll != 0)
            anim.SetTrigger("Punch");
        else
            anim.SetTrigger("Punch2");
    }
}
