using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Ability : MonoBehaviour
{
    public enum UseType
    {
        Bar, Continuous
    }

    public UseType useType;

    public float cost;
    public float costRequirement;
    public bool isActive;

    public virtual void UseAbility()
    {
        
        if (!isActive && PlayerStats.instance.abilityAmount >= costRequirement) //Checks to see if an ability is active
        {
            if (useType == UseType.Continuous)
                isActive = true;

            Activate();
            DecreaseAbililtyAmount();
        }
    }

    public abstract void Activate();
    public abstract void Deactivate();

    public virtual void DecreaseAbililtyAmount()
    {
        switch (useType)
        {
            case UseType.Bar:
                PlayerStats.instance.abilityAmount -= cost;
                Deactivate();
                break;
            case UseType.Continuous:
                StartCoroutine(ContinuousDecrease());
                break;            
        }
    }

    IEnumerator ContinuousDecrease()
    {
        while (PlayerStats.instance.abilityAmount > 0)
        {
            PlayerStats.instance.abilityAmount -= cost * Time.deltaTime;
            yield return null;
        }
        isActive = false;
        Deactivate();
    }
}
