using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PistolScript : MonoBehaviour
{
    public Transform firePoint;
    public GameObject bulletPrefab;
    private Stamina_Bar staminabar;
    public float cooldownTime;
    public float previousCooldown;
    private bool canShoot;
    public float cost;

    private void Start()
    {
        previousCooldown = cooldownTime;
        staminabar = Stamina_Bar.instance;
    }
    // Update is called once per frame
    void Update()
    {
        if (gameObject.activeSelf)
            PlayerController.instance.moveSpeed = 35f;
        else
            PlayerController.instance.moveSpeed = 25f;

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
        if (Input.GetButtonDown("Fire1") && canShoot)
        {
            if (staminabar.MAX_STAMINA > 20)
            {
                Fire();
            }
        }
    }

    void Fire()
    {
        ObjectPooler.instance.SpawnFromPool("PistolBullet", firePoint.position, firePoint.rotation);
        staminabar.MAX_STAMINA -= cost;
        GetComponentInParent<PlayerStats>().stamina -= cost;
        canShoot = false;
    }
}
