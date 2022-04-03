using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public abstract class LethalObject : MonoBehaviour
{
    public float damage;

    public void DamagePlayer(float dmg, float knockbackDuration, float knockbackStrength, PlayerStats.ShakeAmount shakeAmount)
    {
        PlayerStats.instance.Damage(dmg, shakeAmount);
        //StartCoroutine(CameraScript.instance.Shake(screenShakePower, screenShakeFreqPower, screenShakeDuration));
        StartCoroutine(PlayerController.instance.Knockback(knockbackDuration, knockbackStrength, this.transform));
    }
}
