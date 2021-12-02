using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityController : MonoBehaviour
{
    public Ability currentAbility;

    private void Start()
    {
        
    }

    private void Update()
    {
        if (Input.GetButtonDown("Ability"))
        {
            currentAbility.UseAbility();
        }
    }
}
