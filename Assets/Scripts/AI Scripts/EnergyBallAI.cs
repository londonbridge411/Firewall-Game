using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergyBallAI : MonoBehaviour, IPooledObject
{
    public float damage = 5f;
    public float speed = 25f;
    private Vector3 resumeVelocity;
    private Rigidbody rb;

    public void OnObjectSpawn()
    {
        rb = GetComponent<Rigidbody>();
        
        if (!GameManager.stoppedTime)
        {
            rb.isKinematic = false;
            rb.velocity = transform.forward * speed;
            StartCoroutine(ObjectPooler.instance.Despawn(gameObject, 3));
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            PlayerStats.instance.Damage(damage, PlayerStats.ShakeAmount.SMALL);
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

    void Start()
    {
        GameManager.instance.OnTimeStop += OnTimeStop;
        GameManager.instance.OnTimeResume += OnTimeResume;
    }

    void OnTimeStop()
    {
        resumeVelocity = rb.velocity;
    }

    void OnTimeResume()
    {
        rb.isKinematic = false;
        if (resumeVelocity == Vector3.zero)
            rb.velocity = transform.forward * speed;
        else
            rb.velocity = resumeVelocity;
        if (gameObject.activeSelf)
            StartCoroutine(ObjectPooler.instance.Despawn(gameObject, 3));
    }
}
