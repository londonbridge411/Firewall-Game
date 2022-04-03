using Cinemachine;
using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    public static CameraScript instance;
    private CinemachineBasicMultiChannelPerlin cameraNoise;
    public bool isShaking;
    private Animator anim;

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
        //if (isShaking && )
    }



    public IEnumerator Shake(float amplitudeGain, float frequencyGain, float time)
    {
        
        if (cameraNoise != null && isShaking == false)
        {
            isShaking = true;
            cameraNoise.m_AmplitudeGain = amplitudeGain;
            cameraNoise.m_FrequencyGain = frequencyGain;
            yield return new WaitForSeconds(time);
        }
        isShaking = false;
        cameraNoise.m_AmplitudeGain = 0f;
        cameraNoise.m_FrequencyGain = 0f;

    }
}
