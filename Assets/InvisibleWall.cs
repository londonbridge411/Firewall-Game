using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvisibleWall : MonoBehaviour
{
    public float value;
    public bool isInvisible;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if ((collision.collider.tag.Equals("Ammo") || collision.collider.tag.Equals("Sword")) && !isInvisible)
        {
            print("AAA");
            StartCoroutine(Disappear());
            isInvisible = true;
        }
    }

    IEnumerator Disappear()
    {
        if (gameObject.GetComponent<MeshRenderer>())
        {
            Material currentMaterial = gameObject.GetComponent<MeshRenderer>().material;

            for (float t = 0; t < 1; t += Time.deltaTime / 0.5f)
            {
                value = Mathf.Lerp(1f, 0f, t); //(1.05f, -0.05f
                currentMaterial.SetFloat("Vector1_74A4DCF8", value);
                yield return new WaitForFixedUpdate();
            }
            gameObject.SetActive(false);
        }
    }
}
