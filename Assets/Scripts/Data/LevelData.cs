using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using bowen.Saving;

public class LevelData : MonoBehaviour, ISaveable
{
    //Level
    public static int levelNumber;

    public static void SetLevel()
    {
        levelNumber = SceneManager.GetActiveScene().buildIndex;
    }

    public object CaptureState()
    {
        //levelNumber = SceneManager.GetActiveScene().buildIndex;

        return new SaveData
        {
            levelNumber = levelNumber
        };   
    }

    public void RestoreState(object state)
    {
        var saveData = (SaveData)state;

        levelNumber = saveData.levelNumber;
    }

    [Serializable]
    private struct SaveData
    {
        public int levelNumber;
    }
}
