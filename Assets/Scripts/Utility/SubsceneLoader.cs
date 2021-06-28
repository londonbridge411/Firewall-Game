using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SubsceneLoader : MonoBehaviour
{
    public int index;
    public bool isLoaded;
    public float loadDistance;

    private void Update()
    {
        if (Vector3.Distance(transform.position, PlayerController.instance.transform.position) < loadDistance)
        {
            if (!isLoaded)
            {
                StartCoroutine(GoToScene(index));
                isLoaded = true;
            }
        }
        else
        {
            if (isLoaded)
            {
                StartCoroutine(UnloadScene(index));
                isLoaded = false;
            }
        }
    }

    public IEnumerator GoToScene(int levelIndex)
    {
        var progress = SceneManager.LoadSceneAsync(levelIndex, LoadSceneMode.Additive);

        if (!progress.isDone)
        {
            yield return null;
        }
    }

    public IEnumerator UnloadScene(int levelIndex)
    {
        var progress = SceneManager.UnloadSceneAsync(levelIndex);

        if (!progress.isDone)
        {
            yield return null;
        }
    }
}
