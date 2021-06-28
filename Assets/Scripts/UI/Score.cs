using System.Collections;
using System.Collections.Generic;
using bowen.Saving;
using UnityEngine;
using TMPro;
using System;

public class Score : MonoBehaviour //, ISaveable
{

    private TextMeshProUGUI scoreText;
    [SerializeField] public int score;

    #region Singleton
    public static Score instance;

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

    // Start is called before the first frame update
    void Start()
    {
        scoreText = GetComponent<TextMeshProUGUI>();
    }

    private void Update()
    {
        if (MenuControl.instance.isPaused)
        {
            return;
        }

        scoreText.text = score.ToString();
    }

    public void AddPoints(int points)
    {
        score += points;
    }

    public void RemovePoints(int points)
    {
        score -= points;
    }

    /*public object CaptureState()
    {
        return new SaveData
        {
            score = score
        };
    }

    public void RestoreState(object state)
    {
        var saveData = (SaveData)state;

        score = saveData.score; //Set to 0 if it starts multiplying and shit
    }

    [Serializable]
    private struct SaveData
    {
        public int score;
    }*/
}
