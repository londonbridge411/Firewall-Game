using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class Blast : MonoBehaviour, IPooledObject
{
    //public static Blast instance;
    public float damage = 5f;
    public float explosionDamage = 2f;
    public float speed = 10f;
    public Rigidbody rb;
    public GameObject explosionEffect;
    public GameObject blast;
    public float force;
    public float radius;

    private Vector3 resumeVelocity;
    private List<ContactPoint> cp = new List<ContactPoint>();

    // Start is called before the first frame update
    private void Start()
    {
        GameManager.instance.OnTimeStop += OnTimeStop;
        GameManager.instance.OnTimeResume += OnTimeResume;
    }

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
        if (collision.gameObject.tag == "Enemy")
        {
            collision.GetContacts(cp);
            if (!cp.Where(c => c.otherCollider != null).Any(c => c.otherCollider.gameObject.CompareTag("Shield")))
            {
                GameObject enemy = collision.gameObject;
                enemy.GetComponent<AiStats>().TakeDamage(damage);
                Explosion();
                ObjectPooler.instance.Despawn(gameObject);
            }
            else
            {
                print("BLOCKED");
                GameObject shield = collision.gameObject;
                shield.GetComponentInChildren<Shield>().TakeDamage(damage);
            }
        }

        if (collision.gameObject.tag.Equals("Boss"))
        {
            collision.GetContacts(cp);
            if (!cp.Where(c => c.otherCollider != null).Any(c => c.otherCollider.gameObject.CompareTag("Shield")))
            {
                GameObject enemy = collision.gameObject;
                enemy.GetComponent<BossStats>().TakeDamage(damage);
                ObjectPooler.instance.Despawn(gameObject);
            }
            else
            {
                print("BLOCKED");
                GameObject shield = collision.gameObject;
                shield.GetComponentInChildren<Shield>().TakeDamage(damage);
            }
        }
    }

    void Explosion()
    {
        GameObject blastEffect = Instantiate(explosionEffect, transform.position, transform.rotation);
        Collider[] colliders = Physics.OverlapSphere(transform.position, radius);
        foreach (Collider obj in colliders)
        {
            Rigidbody rigidbody = obj.GetComponent<Rigidbody>();
            if (rigidbody != null)
            {
                rigidbody.AddExplosionForce(force, transform.position.normalized, radius);
            }
            if (obj.gameObject.tag == "Enemy")
            {
                GameObject enemy = obj.gameObject;
                enemy.GetComponent<AiStats>().TakeDamage(explosionDamage);
            }
        }
        Destroy(blastEffect, 5);
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

