using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleButton : MonoBehaviour
{
    public PuzzleButtonResponse response;
    public int value;
    public bool canPress = true;
    private Animator anim;

    private void Start()
    {
        anim = GetComponentInParent<Animator>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        string colTag = collision.collider.tag;
        if (colTag.Equals("Ammo") || colTag.Equals("Player") && canPress)
        {
            if (colTag.Equals("Ammo"))
                ObjectPooler.instance.Despawn(collision.gameObject);

            //Send Number to puzzle key
            response.Receive(value);
            anim.SetTrigger("Pressed");
            canPress = false;
            StartCoroutine(WaitForAnimation());
        }
    }

    IEnumerator WaitForAnimation()
    {
        yield return new WaitForSeconds(4);
        canPress = true;
    }
}
