using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BHBullet : MonoBehaviour, IPooledObject
{
    public float damage = 5f;
    public string objectPooledName;
    public float speed;
    private Vector3 resumeVelocity;
    private Rigidbody rb;
    private bool tracking;

    /*// Start is called before the first frame update
    public void OnObjectSpawn()
    {
        rb = GetComponent<Rigidbody>();
        rb.velocity = transform.forward * speed;
        StartCoroutine(ObjectPooler.instance.Despawn(gameObject, 1));
    }*/

    /*public void Start()
    {
        
        rb = GetComponent<Rigidbody>();
        rb.velocity = transform.forward * speed;

        if (tracking == false)
            Destroy(gameObject, 2f);
    }*/

    private void Update()
    {
        if (tracking)
        {
            StartCoroutine(Track(1, PlayerController.instance.transform.position));
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag.Equals("Wall"))
        {                                               
            ObjectPooler.instance.Despawn(gameObject);
        }

        if (other.tag.Equals("Player"))
        {
            PlayerStats.instance.Damage(damage);
            //Destroy(gameObject);
            ObjectPooler.instance.Despawn(gameObject);
        }
    }

    public void SetTracking(bool value)
    {
        tracking = value;
    }

    public void SetSpeed(float s)
    {
        speed = s;
    }

    private IEnumerator Track(float time, Vector3 target)
    {
        float randTime = Random.Range(0.5f, 1.5f);
        yield return new WaitForSeconds(randTime);
        Vector3 AiPosition = transform.position;
        StartCoroutine(TrackBehavior(time, target));
    }

    private IEnumerator TrackBehavior(float time, Vector3 targetPos)
    {
        if (tracking)
        {
            for (float t = 0; t < 1; t += Time.deltaTime / time)
            {
                transform.position = Vector3.MoveTowards(transform.position, PlayerController.instance.transform.position, speed * Time.deltaTime);

                if (Vector3.Distance(transform.position, PlayerController.instance.transform.position) < 5)
                {
                    Vector3 target = PlayerController.instance.transform.position;
                    StartCoroutine(EndTracking(targetPos));
                }
                yield return null;
            }
        }
    }

    private IEnumerator EndTracking(Vector3 targetPos)
    {
        for (float t = 0; t < 1; t += Time.deltaTime / 1)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPos, speed * Time.deltaTime);
            yield return null;
            ObjectPooler.instance.Despawn(gameObject);
        }
        print("Destroy ball");  
    }

    private void Start()
    {
        GameManager.instance.OnTimeStop += OnTimeStop;
        GameManager.instance.OnTimeResume += OnTimeResume;
    }
    //Do this stuff for stopped Objects
    public void OnObjectSpawn()
    {
        rb = GetComponent<Rigidbody>();

        if (!GameManager.stoppedTime)
        {
            rb.isKinematic = false;
            rb.velocity = transform.forward * speed;
            StartCoroutine(ObjectPooler.instance.Despawn(gameObject, 2));
        }
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
            StartCoroutine(ObjectPooler.instance.Despawn(gameObject, 2));
    }
}
