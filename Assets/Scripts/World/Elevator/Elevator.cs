using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class Elevator : MonoBehaviour
{
    #region TODO
    /*
     * Doors open on Interact Button when near OR open automatically.
     * Door open animation.
     * Elevator to travel to designated spot.
     * Elevator shake when moving
     */
    #endregion

    public bool isTraveling;
    [SerializeField] public bool canMove;
    [SerializeField] private ElevatorDoor[] elevatorDoors;

    public Transform pointA;

    public float speed;
    private float calculatedTime;
    Vector3 posA;
    Vector3 posB;
    [SerializeField] private bool atPointA;
    [SerializeField] private bool atPointB;

    // Start is called before the first frame update
    void Start()
    {
        elevatorDoors = GetComponentsInChildren<ElevatorDoor>();
        posA = new Vector3(transform.position.x, pointA.position.y, transform.position.z);
        posB = transform.position;
        calculatedTime = (Vector3.Distance(transform.position, posA) / speed) * 0.1f;
    }

    // Update is called once per frame
    void Update()
    {
        if (isTraveling)
            PlayerController.instance.canMove = false;
        else
            PlayerController.instance.canMove = true;

        if (Vector3.Distance(transform.position, posA) < 1)
            atPointA = true;
        else
            atPointA = false;

        if (Vector3.Distance(transform.position, posB) < 1)
            atPointB = true;
        else
            atPointB = false;
    }

    private void OnTriggerStay(Collider col)
    {
        if (col.tag.Equals("Player"))
        {
            foreach (ElevatorDoor door in elevatorDoors)
            {
                if (door.isOpen)
                {
                    canMove = false;
                    return;
                }
                else
                {
                    //i++;
                }
                break;
            }

            //if (i == elevatorDoors.Length)
                canMove = true;
        }
    }
   
    private void OnTriggerExit(Collider col)
    {
        if (col.tag.Equals("Player"))
        {
            canMove = false;
        }
    }

    public void Move()
    {
        if (canMove)
        {
            if (atPointB)
                StartCoroutine(Move(posA));

            if (atPointA)
                StartCoroutine(Move(posB));
        }
    }

    IEnumerator Move(Vector3 point)
    {


        Vector3 pos = transform.position;
        isTraveling = true;
        //Vector3 pos = transform.position - bottom.position;
        for (float t = 0; t < 1; t += Time.deltaTime / calculatedTime)
        {
            while (GameManager.stoppedTime)
            {
                yield return new WaitUntil(() => GameManager.stoppedTime);
            }
            transform.position = Vector3.Lerp(pos, point, t);
            yield return new WaitForFixedUpdate();
        }
        isTraveling = false;

    }
}
