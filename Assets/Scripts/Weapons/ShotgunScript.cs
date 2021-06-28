using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotgunScript : MonoBehaviour
{
    public Transform firePoint;
    public Transform firePoint2;

    public GameObject bulletPrefab;
    public GameObject bullet2Prefab;

    private Stamina_Bar staminabar;
    private Rigidbody rb;

    public float cooldownTime;
    private float previousCooldown;
    private bool canShoot;

    private float cooldownTime2;
    private float previousCooldown2;
    private bool canShoot2;

    Vector3 moveDirection;
    private bool IsKnockbacked;

    public float cost;

    private void Start()
    {
        staminabar = Stamina_Bar.instance;
        rb = PlayerStats.instance.gameObject.GetComponent<Rigidbody>();
        cooldownTime2 = cooldownTime;
        previousCooldown = cooldownTime;
        previousCooldown2 = cooldownTime2;
        canShoot = true;
        canShoot2 = true;
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

        //Knockback
        if (IsKnockbacked && (cooldownTime == 1.5 || cooldownTime2 == 1.5f))
            //StartCoroutine(Knockback());

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

        if (canShoot2 == false)
        {
            cooldownTime2 -= Time.deltaTime;
            if (cooldownTime2 <= 0)
            {
                canShoot2 = true;
                cooldownTime2 = previousCooldown2;
            }
        }

        //Barrel 1
        if (Input.GetButton("Fire1") && canShoot)
        {
            float predictedStamina = staminabar.MAX_STAMINA - cost;
            if (predictedStamina > 20)
            {
                Fire1();
                IsKnockbacked = true;
                StartCoroutine(CameraScript.instance.Shake(2, 2, .5f));
            }
        }

        //Barrel 2
        if (Input.GetButton("Fire2") && canShoot2)
        {
            float predictedStamina = staminabar.MAX_STAMINA - cost;
            if (predictedStamina > 20)
            {
                Fire2();
                IsKnockbacked = true;
                StartCoroutine(CameraScript.instance.Shake(2, 2, .5f));
            }
        }
    }

    void Fire1()
    {
        moveDirection = rb.transform.position - this.transform.position; //Knockback
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        foreach (Transform g in bullet.transform)
        {
            float randomRotation = Random.Range(0f, 30f);
            g.transform.Rotate(Vector3.up * (randomRotation-15));
        }
        staminabar.MAX_STAMINA -= cost;
        PlayerStats.instance.stamina -= cost;
        canShoot = false;
    }

    void Fire2()
    {
        moveDirection = rb.transform.position - this.transform.position; //Knockback
        GameObject bullet = Instantiate(bulletPrefab, firePoint2.position, firePoint2.rotation); //ObjectPooler.instance.SpawnFromPool("Pellets", firePoint2.position, firePoint2.rotation);
        foreach (Transform g in bullet.transform)
        {
            float randomRotation = Random.Range(0f, 30f);
            g.transform.Rotate(Vector3.up * (randomRotation - 15));
        }
        staminabar.MAX_STAMINA -= cost;
        PlayerStats.instance.stamina -= cost;
        canShoot2 = false;
    }

    IEnumerator Knockback()
    {
        rb.AddForce(moveDirection.normalized * -1000f);
        yield return new WaitForSecondsRealtime(0.25f);
        IsKnockbacked = false;
    }
}

