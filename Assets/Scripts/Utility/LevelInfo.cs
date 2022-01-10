using System;
using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using bowen.Saving;

public class LevelInfo : MonoBehaviour, ISaveable
{
    [Header("Scene Loading")]
    public int levelIndex;
    public bool isLoaded;
    public float loadDistance;

    [Header("Enemy Respawn")]
    public float timeAway;
    public int maxSpawnerCount;
    public EnemySpawner[] spawners;

    private float respawnTime = 20;

    private void Start()
    {
        Invoke("SpawnEnemies", 0.1f);
    }

    private void Update()
    { 
        if (Vector3.Distance(transform.position, PlayerController.instance.transform.position) < loadDistance)
        {
            if (!isLoaded)
            {
                StartCoroutine(GoToScene(levelIndex));
                isLoaded = true;

                if (timeAway >= respawnTime)
                { 
                    foreach (EnemySpawner s in spawners)
                    {
                        if (s.spawnState == EnemySpawner.SpawnerState.Dead || s.spawnState == EnemySpawner.SpawnerState.Psuedo_Dead)
                            s.spawnState = EnemySpawner.SpawnerState.Empty;
                    }
                    SpawnEnemies();
                }
            }       
        }
        else
        {
            if (isLoaded)
            {
                StartCoroutine(UnloadScene(levelIndex));
                isLoaded = false;
            }
            timeAway += Time.deltaTime;
        }

        if (timeAway >= respawnTime)
        {
            foreach (EnemySpawner s in spawners)
            {
                //if (s.spawnState == EnemySpawner.SpawnerState.Dead)
                //    s.spawnState = EnemySpawner.SpawnerState.Empty;

                /*if (s.spawnState == EnemySpawner.SpawnerState.Psuedo_Dead)
                {
                    Destroy(s.spawnedObject);
                    s.spawnState = EnemySpawner.SpawnerState.Empty;
                }*/
            }
        }
    }

    void SpawnEnemies()
    {
        timeAway = 0;
        if (spawners.Length < maxSpawnerCount)
            throw new Exception("There are not enough spawners.");

        int spawnsFilled = 0;

        foreach (EnemySpawner s in spawners)
        {
            if (s.spawnState == EnemySpawner.SpawnerState.Filled || s.spawnState == EnemySpawner.SpawnerState.Psuedo_Filled)
            {

                if (s.spawnedObject == null)
                {
                    s.SpawnEnemy();
                }
                spawnsFilled++;
            }


            //if (s.spawnState == EnemySpawner.SpawnerState.Dead)
               // s.spawnState = EnemySpawner.SpawnerState.Empty;
        }

        while (spawnsFilled < maxSpawnerCount) //While
        {
            //Gets a random spawner position.
            int rand = UnityEngine.Random.Range(0, spawners.Length);

            // Regets the spawner position in case the spawner is already filled
            while (spawners[rand].spawnState == EnemySpawner.SpawnerState.Filled || spawners[rand].spawnState == EnemySpawner.SpawnerState.Psuedo_Filled) //While
                rand = UnityEngine.Random.Range(0, spawners.Length);

            spawners[rand].SpawnEnemy();

            spawnsFilled++;
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

    public object CaptureState()
    {
        return new SaveData
        {
            timeAway = timeAway
        };
    }

    public void RestoreState(object state)
    {
        var saveData = (SaveData)state;

        timeAway = saveData.timeAway;
    }

    [Serializable]
    private struct SaveData
    {
        public float timeAway;
    }
}
