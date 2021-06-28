using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlasterScript : MonoBehaviour
{
    public Transform firePoint;
    public GameObject smallBlast;
    public GameObject normalBlast;
    public GameObject bigBlast;
    public GameObject firedObject;
    private Stamina_Bar staminabar;
    private Animator anim;
    public float cooldownTime;
    private float previousCooldown;
    private bool canShoot;
    public float cost;
    private float startTime;
    private float endTime;

    private void Start()
    {
        anim = GetComponent<Animator>();
        previousCooldown = cooldownTime;
        cooldownTime = 0;
        staminabar = Stamina_Bar.instance;
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
        if (Input.GetButtonDown("Fire1") && canShoot == true)
        {
            startTime = Time.time;
        }

        if (Input.GetButtonUp("Fire1") && canShoot == true)
        {
            endTime = Time.time;
            float chargedTime = endTime - startTime;
            float predictedStamina = staminabar.MAX_STAMINA - cost;
            if (predictedStamina > 20)
            {

                Fire(chargedTime);
            }
        }
    }

    void Fire(float time)
    {
        if (time < .5)
        {
            ObjectPooler.instance.SpawnFromPool("SmallBlast", firePoint.position, firePoint.rotation);
            //Blast.instance.blast = FiredObject(smallBlast);
            StartCoroutine(CameraScript.instance.Shake(2f, 1, .2f));
            staminabar.MAX_STAMINA -= cost;
            PlayerStats.instance.stamina -= cost;
            canShoot = false;
        }

        if (time >= .5 && time <= 2)
        {
            ObjectPooler.instance.SpawnFromPool("Blast", firePoint.position, firePoint.rotation);
            //Blast.instance.blast = FiredObject(normalBlast);
            StartCoroutine(CameraScript.instance.Shake(3f, 2f, .2f));
            staminabar.MAX_STAMINA -= cost * 2f;
            PlayerStats.instance.stamina -= cost * 2f;
            canShoot = false;
        }

        if (time > 2)
        {
            ObjectPooler.instance.SpawnFromPool("BigBlast", firePoint.position, firePoint.rotation);
            //Blast.instance.blast = FiredObject(bigBlast);
            StartCoroutine(CameraScript.instance.Shake(8f, 3f, .7f));
            staminabar.MAX_STAMINA -= cost * 3f;
            PlayerStats.instance.stamina -= cost * 3f;
            canShoot = false;
        }
        anim.SetTrigger("Charging");
    }

    public GameObject FiredObject(GameObject obj)
    {
        return firedObject = obj;
    }
}
