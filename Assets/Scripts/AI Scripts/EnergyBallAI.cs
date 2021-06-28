using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergyBallAI : MonoBehaviour
{
    public float damage = 5f;
    public float speed = 25f;
    private Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.velocity = transform.forward * speed;
        Destroy(gameObject, 3);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            PlayerStats.instance.Damage(damage);
        }
        if (collision.gameObject.tag != "Ammo")
        {
            ObjectPooler.instance.Despawn(gameObject);
        }
        else
        {
            ObjectPooler.instance.Despawn(collision.gameObject);
        }
    }
}
