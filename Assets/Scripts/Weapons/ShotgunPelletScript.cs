using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class ShotgunPelletScript : MonoBehaviour
{
    public float damage = 5f;
    public float speed = 50f;
    private Rigidbody rb;
    private List<ContactPoint> cp = new List<ContactPoint>();

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();

        if (GameManager.stoppedTime)
            StartCoroutine(StopTime());
        else
        {
            rb.isKinematic = false;
            rb.velocity = transform.forward * speed;
            StartCoroutine(CustomDestroy(1));
        }
    }

    // Update is called once per frame
    void Update()
    {

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
                Destroy(gameObject);
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
                Destroy(gameObject);
            }
            else
            {
                print("BLOCKED");
                GameObject shield = collision.gameObject;
                shield.GetComponentInChildren<Shield>().TakeDamage(damage);
            }
        }
    }

    public IEnumerator CustomDestroy(float time)
    {
        float startTime = 0;

        while (startTime < time)
        {
            while (GameManager.stoppedTime)
            {
                yield return new WaitUntil(() => GameManager.stoppedTime);
            }

            startTime += Time.deltaTime;
            yield return null;
        }

        Destroy(gameObject);    
    }

    //I have to do it this way otherwise the whole time stop freeze screws up
    IEnumerator StopTime()
    {
        yield return new WaitWhile(() => GameManager.stoppedTime);
        rb.velocity = transform.forward * speed;
        StartCoroutine(CustomDestroy(1));
    }
}
