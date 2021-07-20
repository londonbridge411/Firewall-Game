using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using bowen.Saving;
using UnityEngine;

public class PlayerData : MonoBehaviour, ISaveable
{
    #region Singleton
    public static PlayerData instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    #endregion

    static PlayerStats player;

    //Position
    static float x;
    static float y;
    static float z;

    //Score
    static int score;

    //Weapon Upgrades

    public static void SetPosition(GameObject obj)
    {
        if (player == null)
        {
            player = FindObjectOfType<PlayerStats>();
        }
        x = obj.transform.position.x;
        y = player.transform.position.y;
        z = obj.transform.position.z;
    }

    public static void SetScore(int number)
    {
        score = number;
    }

    public object CaptureState()
    {
        return new SaveData
        {
            x = x,
            y = y,
            z = z,
            score = score
        };
    }

    public void RestoreState(object state)
    {
        var saveData = (SaveData)state;

        score = saveData.score;
        Score.instance.score = score;
        if (LevelData.levelNumber == SceneManager.GetActiveScene().buildIndex)
        {
            x = saveData.x;
            y = saveData.y;
            z = saveData.z;
            PlayerStats.instance.transform.position = new Vector3(x, y, z);
        }
        else
        {
            PlayerStats.instance.transform.position = new Vector3(x, PlayerStats.instance.transform.position.y, z);
        }    
    }

    [Serializable]
    private struct SaveData
    {
        //Position
        public float x;
        public float y;
        public float z;

        //Score
        public int score;

        //Weapon Upgrades

    }
}
