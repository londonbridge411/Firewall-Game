using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{

    public enum moveDirection
    {
        up, down, left, right, top, bottom, stopped
    };

    [SerializeField] private moveDirection direction;
    private Vector3 moveVector;

    public Transform[] points;
    public int pointNumber = 0;
    private Vector3 currentTarget;
    Vector3 directionVector;

    public float tolerance;
    public float speed;
    public float delayTime;

    private float delayStart;

    public bool isAutomatic;
    public bool isMoving;

    public bool checkDirection;

    private void Start()
    {
        if (points.Length > 0)
        {
            currentTarget = points[0].position;
        }

        tolerance = speed * Time.deltaTime;
    }

    private void Update()
    {


        if (transform.position != currentTarget)
        {
            Move();
        }
        else
        {
            checkDirection = false;
            UpdateTarget();
        }
    }

    private void Move()
    {
        if (!checkDirection)
        {
            StartCoroutine(Timer(0.01f));
        }

        Vector3 heading = currentTarget - transform.position;
        isMoving = true;
        transform.position += heading.normalized * speed * Time.deltaTime;

        if (heading.magnitude < tolerance)
        {
            transform.position = currentTarget;
            isMoving = false;
            delayStart = Time.time;
        }
    }

    private void UpdateTarget()
    {
        print("Updating target");
        if (isAutomatic)
        {
            if (Time.time - delayStart > delayTime)
            {
                NextPlatform();
            }
        }
    }

    public void NextPlatform()
    {
        pointNumber++;
        if (pointNumber >= points.Length)
        {
            pointNumber = 0;
        }
        currentTarget = points[pointNumber].position;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag.Equals("Player") && isAutomatic == false)
            NextPlatform();
    }

    private void OnCollisionStay(Collision collision)
    {
        //collision.transform.position += moveVector;
        MoveObject(collision.transform);
        //collision.collider.transform.SetParent(gameObject.transform, true);

    }

    void MoveObject(Transform t)
    {
        if (isMoving)
        {
            /*Vector3 heading = currentTarget - t.position;
            t.position += heading.normalized * speed * Time.deltaTime;*/

            Vector3 heading = currentTarget - t.position;
            t.position += moveVector.normalized * speed * Time.deltaTime;
        }
    }

    IEnumerator Timer(float time)
    {
        checkDirection = true;
        Vector3 posA = transform.position;
        print("Vector 1 is " + posA);
        yield return new WaitForSeconds(time);
        Vector3 posB = transform.position;
        print("Vector 2 is " + posB);

        Vector3 directionVector = posB - posA;
        print(directionVector);
        if (directionVector.z < 0)
        {
            direction = moveDirection.down;
        }
        else if (directionVector.z > 0)
        {
            direction = moveDirection.up;
        }
        else if (directionVector.x < 0)
        {
            direction = moveDirection.left;
        }
        else if (directionVector.x > 0)
        {
            direction = moveDirection.right;
        }
        else if (directionVector.y < 0)
        {
            direction = moveDirection.bottom;
        }
        else if (directionVector.y > 0)
        {
            direction = moveDirection.top;
        }
        else
        {
            direction = moveDirection.stopped;
        }

        SetMoveVector();
    }

    void SetMoveVector()
    {
        switch (direction)
        {
            case moveDirection.up:
                moveVector = new Vector3(0, 0, speed);
                break;
            case moveDirection.down:
                moveVector = new Vector3(0, 0, -speed);
                break;
            case moveDirection.left:
                moveVector = new Vector3(-speed, 0, 0);
                break;
            case moveDirection.right:
                moveVector = new Vector3(speed, 0, 0);
                break;
            case moveDirection.top:
                moveVector = new Vector3(0, speed, 0);
                break;
            case moveDirection.bottom:
                moveVector = new Vector3(0, -speed, 0);
                break;
            case moveDirection.stopped:
                moveVector = Vector3.zero;
                break;
        }
    }
}
