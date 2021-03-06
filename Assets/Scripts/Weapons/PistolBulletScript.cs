using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PistolBulletScript : MonoBehaviour, IPooledObject
{
    public float damage = 5f;
    public float speed = 50f;
    public Rigidbody rb;
    List<ContactPoint> cp = new List<ContactPoint>();

    private Vector3 resumeVelocity;

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
            StartCoroutine(ObjectPooler.instance.Despawn(gameObject, 1));
    }

    // Start is called before the first frame update
    public void OnObjectSpawn()
    {
        rb = GetComponent<Rigidbody>();

        if (!GameManager.stoppedTime)
        {
            rb.isKinematic = false;
            rb.velocity = transform.forward * speed;
            StartCoroutine(ObjectPooler.instance.Despawn(gameObject, 1));
        }
    }


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            collision.GetContacts(cp);
            if (!cp.Where(c => c.otherCollider != null).Any(c => c.otherCollider.gameObject.CompareTag("Shield")))
            {
                GameObject enemy = collision.gameObject;
                enemy.GetComponent<AiStats>().TakeDamage(damage);
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
}
