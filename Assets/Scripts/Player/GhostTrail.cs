using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostTrail : MonoBehaviour
{
    public float delay = .01f;
    float delta = 0;

    PlayerController player;
    public Material material = null;


    // Start is called before the first frame update
    void Start()
    {
        player = PlayerController.instance;
    }

    // Update is called once per frame
    void Update()
    {
        if (delta > 0)
        {
            delta -= Time.deltaTime;
        }
        else
        {
            delta = delay;
            createGhost();
        }
    }

    void createGhost()
    {
        if (gameObject.GetComponent<WeaponSwapper>().weapons[gameObject.GetComponent<WeaponSwapper>().WeaponNumber].name.Equals("MachineGun"))
        {
            GameObject ghostObj = ObjectPooler.instance.SpawnFromPool("GhostLMG", transform.position, transform.rotation);
            ghostObj.transform.localScale = gameObject.transform.localScale;
            StartCoroutine(ObjectPooler.instance.Despawn(ghostObj, .25f));
        }
        if (gameObject.GetComponent<WeaponSwapper>().weapons[gameObject.GetComponent<WeaponSwapper>().WeaponNumber].name.Equals("Blaster"))
        {
            GameObject ghostObj = ObjectPooler.instance.SpawnFromPool("GhostBlaster", transform.position, transform.rotation);
            ghostObj.transform.localScale = gameObject.transform.localScale;
            StartCoroutine(ObjectPooler.instance.Despawn(ghostObj, .25f));
        }
        if (gameObject.GetComponent<WeaponSwapper>().weapons[gameObject.GetComponent<WeaponSwapper>().WeaponNumber].name.Equals("Pistol"))
        {
            GameObject ghostObj = ObjectPooler.instance.SpawnFromPool("GhostPistol", transform.position, transform.rotation);
            ghostObj.transform.localScale = gameObject.transform.localScale;
            StartCoroutine(ObjectPooler.instance.Despawn(ghostObj, .25f));
        }
        if (gameObject.GetComponent<WeaponSwapper>().weapons[gameObject.GetComponent<WeaponSwapper>().WeaponNumber].name.Equals("Shotgun"))
        {
            GameObject ghostObj = ObjectPooler.instance.SpawnFromPool("GhostShotgun", transform.position, transform.rotation);
            ghostObj.transform.localScale = gameObject.transform.localScale;
            StartCoroutine(ObjectPooler.instance.Despawn(ghostObj, .25f));
        }
    }
}
