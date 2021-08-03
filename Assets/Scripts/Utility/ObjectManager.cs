using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using bowen.Saving;

public class ObjectManager : MonoBehaviour, ISaveable
{
    [SerializeField] private bool isDisabled;
    private Transform child;

    private void Awake()
    {
        child = transform.GetChild(0);
    }

    private void Update()
    {
        if (isDisabled)
        {
            child.gameObject.SetActive(false);
        }
        else
            child.gameObject.SetActive(true);
        //Causes problems with item loading. The method below was used when the levels were not connected.
        /*if (SceneManager.GetSceneByBuildIndex(sceneID).isLoaded)
        {
            if (isDisabled)
            {
                child.SetActive(false);
            }
            else
                child.SetActive(true);
        }
        else
            child.SetActive(false);*/
    }

    public void DisableObject()
    {
        isDisabled = true;
    }

    public object CaptureState()
    {
        return new SaveData
        {
            isDisabled = isDisabled,
        };
    }

    public void RestoreState(object state)
    {
        var saveData = (SaveData)state;
        isDisabled = saveData.isDisabled;
    }

    [Serializable]

    private struct SaveData
    {
        public bool isDisabled;
    }
}
