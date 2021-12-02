using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShellDisappear : MonoBehaviour
{
    float value;

    // Update is called once per frame
    void OnEnable()
    {
        MeshRenderer rend = gameObject.GetComponent<MeshRenderer>();
        rend.material = new Material(rend.material);
        Material curMat = rend.material;

        curMat.SetFloat("Vector1_74A4DCF8", 1);

        if (GameManager.stoppedTime)
        {
            StartCoroutine(StopTime());
        }
        else
            StartCoroutine(ShellFade());
    }

    IEnumerator StopTime()
    {
        yield return new WaitWhile(() => GameManager.stoppedTime);
        StartCoroutine(ShellFade());
    }

    IEnumerator ShellFade()
    {
        yield return new WaitForSeconds(3.5f);
        MeshRenderer renderer = gameObject.GetComponent<MeshRenderer>();
        renderer.material = new Material(renderer.material);
        Material currentMaterial = renderer.material;

        for (float t = 0; t < 1; t += Time.deltaTime / 1.5f)
        {
            while (GameManager.stoppedTime)
            {
                yield return new WaitUntil(() => GameManager.stoppedTime);
            }

            value = Mathf.Lerp(1f, 0, t);
            currentMaterial.SetFloat("Vector1_74A4DCF8", value);
            yield return new WaitForFixedUpdate();
        }
        ObjectPooler.instance.Despawn(gameObject);
    }
}
