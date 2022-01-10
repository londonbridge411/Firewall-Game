using System.Collections;
using System.Collections.Generic;
using UnityEngine.Rendering.Universal;
using UnityEngine.Rendering;
using UnityEngine;
using bowen.Saving;

public class GameManager : MonoBehaviour
{
    public static bool stoppedTime;
    public static bool overclocked;
    Volume volume;
    static ColorAdjustments colorAdjust;
    static ChromaticAberration chromaticAberration;
    static LensDistortion lensDistortion;

    #region Singleton
    public static GameManager instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        volume = FindObjectOfType<Volume>();
        if (volume.profile.TryGet(out ColorAdjustments newColorAdjust))
        {
            colorAdjust = newColorAdjust;
        }

        if (volume.profile.TryGet(out ChromaticAberration newChromaticAberration))
        {
            chromaticAberration = newChromaticAberration;
        }

        if (volume.profile.TryGet(out LensDistortion newLensDistortion))
        {
            lensDistortion = newLensDistortion;
        }
        SaveLoadSystem.instance.Load();
    }

    #region Time Stop
    public delegate void OnTimeStopHandler();

    public event OnTimeStopHandler OnTimeStop;
    public event OnTimeStopHandler OnTimeResume;

    public void StoppedTime()
    {
        print("Time has stopped.");
        StartCoroutine(StoppedTimeLerp());
        StartCoroutine(LensEffect());
        StartCoroutine(CameraScript.instance.Shake(1.5f, 2, 1f));
        OnTimeStop?.Invoke();
    }

    public void ResumedTime()
    {
        print("Time has resumed.");
        if (stoppedTime)
        {
            StartCoroutine(StoppedTimeRevert());
            stoppedTime = false;
        }
        OnTimeResume?.Invoke();
    }

    public static IEnumerator StoppedTimeLerp()
    {
        for (float t = 0; t < 1; t += Time.deltaTime / 1.25f)
        {
            colorAdjust.hueShift.value = Mathf.Lerp(0f, 190f, t);
            chromaticAberration.intensity.value = Mathf.Lerp(0f, 1f, t);
            yield return new WaitForFixedUpdate();
            lensDistortion.intensity.value = Mathf.Lerp(-0.75f, 0f, t);
        }

        colorAdjust.hueShift.value = 180f;
        chromaticAberration.intensity.value = 1f;
    }

    public static IEnumerator LensEffect()
    {
        for (float t = 0; t < 1; t += Time.deltaTime / 0.75f)
        {
            lensDistortion.intensity.value = Mathf.Lerp(0f, -0.75f, t);
            yield return new WaitForFixedUpdate();
        }

        for (float t = 0; t < 1; t += Time.deltaTime / 0.5f)
        {
            lensDistortion.intensity.value = Mathf.Lerp(-0.75f, 0f, t);          
            yield return new WaitForFixedUpdate();
        }

        lensDistortion.intensity.value = 0f;
    }

    public static IEnumerator StoppedTimeRevert()
    {
        for (float t = 0; t < 1; t += Time.deltaTime / 0.5f)
        {
            colorAdjust.hueShift.value = Mathf.Lerp(180f, 0f, t);
            chromaticAberration.intensity.value = Mathf.Lerp(1f, 0f, t);
            yield return new WaitForFixedUpdate();
        }

        colorAdjust.hueShift.value = 0f;
        chromaticAberration.intensity.value = 0f;
    }
    #endregion

    #region Overclock

    public static IEnumerator OverclockLerp()
    {
        for (float t = 0; t < 1; t += Time.deltaTime / 1f)
        {
            colorAdjust.colorFilter.value = Color.Lerp(Color.white, Color.red, t);
            yield return new WaitForFixedUpdate();
        }

        colorAdjust.colorFilter.value = Color.red;
    }

    public static IEnumerator OverclockRevert()
    {
        for (float t = 0; t < 1; t += Time.deltaTime / 0.5f)
        {
            colorAdjust.colorFilter.value = Color.Lerp(Color.red, Color.white, t);
            yield return new WaitForFixedUpdate();
        }

        colorAdjust.colorFilter.value = Color.white;
    }
    #endregion

    #region LevelHandler
    public delegate void OnLevelHandler();

    public event OnLevelHandler OnLevelEnter;
    public event OnLevelHandler OnLevelExit;

    public void EnterLevel()
    {
        OnLevelEnter?.Invoke();
    }

    public void ExitLevel()
    {
        OnLevelExit?.Invoke();
    }
    #endregion
}
