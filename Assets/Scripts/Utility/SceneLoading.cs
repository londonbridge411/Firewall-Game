using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using bowen.Saving;

public class SceneLoading : MonoBehaviour
{
    public Image _progressBar;
    private int ToLevel;
    // Start is called before the first frame update
    void Start()
    {
        //start async operation.
        ToLevel = LevelData.levelNumber;
        StartCoroutine(LoadAsyncOperation(ToLevel));
    }

    private void Update()
    {
        print(LevelData.levelNumber);
    }

    IEnumerator LoadAsyncOperation(int index)
    {
        yield return null;

        AsyncOperation gameLevel = SceneManager.LoadSceneAsync(index);

        while (gameLevel.progress < 1)
        {
            _progressBar.fillAmount = gameLevel.progress;
            yield return new WaitForEndOfFrame();
        }

    }
}