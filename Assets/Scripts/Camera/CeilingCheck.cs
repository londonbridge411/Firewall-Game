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

    public IEnumerator SetModeTransparent(GameObject obj, bool setTransparent)
    {
        if (obj.GetComponent<MeshRenderer>())
        {
            foreach (Material currentMaterial in obj.GetComponent<MeshRenderer>().materials)
            {
                if (setTransparent)
                {
                    for (float t = 0; t < 1; t += Time.deltaTime / 0.5f)
                    {
                        value = (obj.tag.Equals("Window")) ? Mathf.Lerp(0.55f, 0f, t) : Mathf.Lerp(1f, 0f, t);
                        currentMaterial.SetFloat("Vector1_74A4DCF8", value);
                        yield return new WaitForFixedUpdate();
                    }
                }
                else
                {
                    for (float t = 0; t < 1; t += Time.deltaTime / 0.5f)
                    {
                        value = (obj.tag.Equals("Window")) ? Mathf.Lerp(0f, 0.55f, t) : value = Mathf.Lerp(0f, 1f, t);
                        currentMaterial.SetFloat("Vector1_74A4DCF8", value);
                        yield return new WaitForFixedUpdate();
                    }
                }
            }
        }
        else
            yield return null;       
    }       
}
