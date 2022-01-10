using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunScript : MonoBehaviour
{
    public Transform firePoint;
    public Transform emptyPoint;
    public GameObject bulletPrefab;
    public GameObject shellPrefab;
    private Stamina_Bar staminabar;
    public float cooldownTime;
    public float previousCooldown;
    private bool canShoot;
    public float cost;

    private void Start()
    {
        previousCooldown = cooldownTime;
        //staminabar = PlayerController.instance.stamina_Bar;
    }
    // Update is called once per frame
    void Update()
    {
        if (MenuControl.instance.isPaused)
        {
            return;
        }

        if (staminabar == null)
        {
            staminabar = PlayerController.instance.stamina_Bar;
        }

        //Cooldown
        if (canShoot == false)
        {
            cooldownTime -= Time.deltaTime;
            if (cooldownTime <= 0)
            {
                canShoot = true;
                cooldownTime = previousCooldown;
            }
        }
        if (Input.GetButton("Fire1") && canShoot)
        {
            float predictedStamina = staminabar.MAX_STAMINA - cost;
            if (predictedStamina > 20)
            {
                Fire();
                StartCoroutine(CameraScript.instance.Shake(3, 2, .5f));
            }
        }
    }

    void Fire()
    {
        ObjectPooler.instance.SpawnFromPool("Bullet", firePoint.position, firePoint.rotation);
        ObjectPooler.instance.SpawnFromPool("Shell", emptyPoint.position, emptyPoint.rotation);
       // AudioManager.instance.PlayOneShot("MachineGunBullet");

        staminabar.MAX_STAMINA -= cost;
        PlayerStats.instance.stamina -= cost;
        canShoot = false;
    }
}
