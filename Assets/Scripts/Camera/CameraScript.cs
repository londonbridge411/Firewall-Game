using Cinemachine;
using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    public static CameraScript instance;
    private CinemachineBasicMultiChannelPerlin cameraNoise;
    private Animator anim;
    float value;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    private void Start()
    {
        cameraNoise = GetComponent<CinemachineVirtualCamera>().GetCinemachineComponent<Cinemachine.CinemachineBasicMultiChannelPerlin>();
        anim = null;
    }

    private void Update()
    {

    }

    private void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Disappear")
        {
            for(int i = 0; i < col.transform.childCount; i++)
            {
                StartCoroutine(SetModeTransparent(col.transform.GetChild(i).gameObject, true));
            }

        }
    }

    private void OnTriggerExit(Collider col)
    {
        if (col.gameObject.tag == "Disappear")
        {
            for (int i = 0; i < col.transform.childCount; i++)
            {
                StartCoroutine(SetModeTransparent(col.transform.GetChild(i).gameObject, false));
            }
        }     
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
                value = Mathf.Lerp(1, 0, t);
                currentMaterial.SetFloat("Vector1_74A4DCF8", value);
                yield return null;
            }
        }
        else
        {
            for (float t = 0; t < 1; t += Time.deltaTime / 0.5f)
            {
                value = Mathf.Lerp(0, 1, t);
                currentMaterial.SetFloat("Vector1_74A4DCF8", value);
                yield return null;
            }
        }
    }

    public IEnumerator Shake(float amplitudeGain, float frequencyGain, float time)
    {
        if (cameraNoise != null)
        {
            cameraNoise.m_AmplitudeGain = amplitudeGain;
            cameraNoise.m_FrequencyGain = frequencyGain;

            yield return new WaitForSeconds(time);
            cameraNoise.m_AmplitudeGain = 0f;
            cameraNoise.m_FrequencyGain = 0f;
        }
        else
        {
            cameraNoise.m_AmplitudeGain = 0f;
            cameraNoise.m_FrequencyGain = 0f;
        }
    }
}
