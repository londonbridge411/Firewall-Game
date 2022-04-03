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
    public bool canRegen;
    [SerializeField]    
    private bool canDamage = true;

    public enum ShakeAmount
    {
        NONE,
        EXTRA_SMALL,
        SMALL,
        MEDIUM,
        LARGE,
        EXTRA_LARGE
    }

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

    public void Damage(float dmg, ShakeAmount amount)
    {
        if (canDamage)
        {
            if (GameManager.overclocked)
                health -= dmg * 1.5f;   
            else
                health -= dmg;
            StartCoroutine(DamageCooldown());

            switch (amount)
            {
                case ShakeAmount.EXTRA_SMALL:
                    StartCoroutine(CameraScript.instance.Shake(1, 2, 0.5f));
                    break;
                case ShakeAmount.SMALL:
                    StartCoroutine(CameraScript.instance.Shake(3, 2, 0.5f));
                    break;
                case ShakeAmount.MEDIUM:
                    StartCoroutine(CameraScript.instance.Shake(6, 3, 0.5f));
                    break;
                case ShakeAmount.LARGE:
                    StartCoroutine(CameraScript.instance.Shake(10, 4, 0.5f));
                    break;
                case ShakeAmount.EXTRA_LARGE:
                    StartCoroutine(CameraScript.instance.Shake(30, 5, 0.5f));
                    break;
            }
            
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

    #region Staminia Handler

    public void LoseStamina(float amount)
    {
        stamina -= amount;
        StartCoroutine(StaminaRegenPause());
    }

    private IEnumerator StaminaRegen()
    {
        while (canRegen && stamina < Stamina_Bar.instance.MAX_STAMINA)
        {
            if (!canRegen)
                break;
            stamina += 20f * Time.deltaTime;
            yield return null;
        }
    }

    private IEnumerator StaminaRegenPause()
    {
        canRegen = false;
        yield return new WaitForSeconds(0.75f);
        canRegen = true;
        StartCoroutine(StaminaRegen());
    }
    #endregion
}
