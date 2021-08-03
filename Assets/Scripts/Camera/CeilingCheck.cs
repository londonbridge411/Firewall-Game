using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CeilingCheck : MonoBehaviour
{
    float value;

    private void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Disappear")
            OpacityControl(col.transform, true);
    }

    private void OnTriggerExit(Collider col)
    {
        if (col.gameObject.tag == "Disappear")
            OpacityControl(col.transform, false);
    }

    void OpacityControl(Transform transform, bool choice)
    {
        if (choice)
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                StartCoroutine(SetModeTransparent(transform.GetChild(i).gameObject, true));
                if (transform.GetChild(i).tag.Equals("Disappear"))
                    OpacityControl(transform.GetChild(i), true);
            }
        }
        else
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                StartCoroutine(SetModeTransparent(transform.GetChild(i).gameObject, false));
                if (transform.GetChild(i).tag.Equals("Disappear"))
                    OpacityControl(transform.GetChild(i), false);
            }
        }
    }
    private void Update()
    {

    }
    public IEnumerator SetModeTransparent(GameObject obj, bool setTransparent)
    {
        MeshRenderer renderer = obj.GetComponent<MeshRenderer>();
        renderer.material = new Material(renderer.material);
        Material currentMaterial = renderer.material;

        if (setTransparent)
        {
            for (float t = 0; t < 1; t += Time.deltaTime / 0.5f)
            {
                if (obj.tag.Equals("Window"))
                    value = Mathf.Lerp(0.55f, 0f, t); //(0.55f, -0.05f
                else
                    value = Mathf.Lerp(1f, 0f, t); //(1.05f, -0.05f
                currentMaterial.SetFloat("Vector1_74A4DCF8", value);
                yield return new WaitForFixedUpdate();
            }
        }
        else
        {
            for (float t = 0; t < 1; t += Time.deltaTime / 0.5f)
            {
                if (obj.tag.Equals("Window"))
                    value = Mathf.Lerp(0f, 0.55f, t);
                else
                    value = Mathf.Lerp(0f, 1f, t);
                currentMaterial.SetFloat("Vector1_74A4DCF8", value);
                yield return new WaitForFixedUpdate();
            }
        }
    }

    void OpacityControl2(Transform transform, bool choice)
    {
        if (choice)
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                StartCoroutine(SetModeTransparent2(transform.GetChild(i).gameObject, true));
                if (transform.GetChild(i).tag.Equals("Disappear"))
                    OpacityControl(transform.GetChild(i), true);
            }
        }
        else
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                StartCoroutine(SetModeTransparent2(transform.GetChild(i).gameObject, false));
                if (transform.GetChild(i).tag.Equals("Disappear"))
                    OpacityControl(transform.GetChild(i), false);
            }
        }
    }

    public IEnumerator SetModeTransparent2(GameObject obj, bool setTransparent)
    {


        MeshRenderer renderer = obj.GetComponent<MeshRenderer>();
        renderer.material = new Material(renderer.material);
        Material currentMaterial = renderer.material;




        if (setTransparent)
        {
            for (float t = 0; t < 1; t += Time.deltaTime / 0.5f)
            {

                value = Mathf.Lerp(1.05f, -0.05f, t);
                currentMaterial.SetFloat("Vector1_74A4DCF8", value);
                yield return null;
            }
        }
        else
        {
            for (float t = 0; t < 1; t += Time.deltaTime / 0.5f)
            {
                value = Mathf.Lerp(-0.05f, 1.05f, t);
                currentMaterial.SetFloat("Vector1_74A4DCF8", value);

                yield return null;
            }
        }
    }
}
