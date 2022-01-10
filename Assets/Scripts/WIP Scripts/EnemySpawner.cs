using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using bowen.Saving;

public class EnemySpawner : MonoBehaviour, ISaveable
{
    public GameObject[] enemies;
    public GameObject spawnedObject { get; private set; }
    //public bool canSpawn = true;
    public int enemyNum;

    public enum SpawnerState
    {
        Empty,
        Filled,
        Psuedo_Filled,
        Psuedo_Dead,
        Dead
    }

    public SpawnerState spawnState = 0;

    private void Start()
    {
        SaveLoadSystem.instance.OnGameLoad += OnReload;
        SaveLoadSystem.instance.OnGameSave += OnSave;
    }

    private void Update()
    {

    }

    private void OnDrawGizmos()
    {
        
    }

    // Method to call a deduce the logic of the spawn.
    public void SpawnEnemy()
    {
        switch (spawnState)
        {
            case SpawnerState.Filled:
                {
                    Spawn();
                    break;
                }
            case SpawnerState.Empty:
                {
                    enemyNum = UnityEngine.Random.Range(0, enemies.Length);
                    Spawn();
                    spawnState = SpawnerState.Psuedo_Filled;
                    break;
                }

        }
    }

    //Actually instantiates the enemy
    void Spawn()
    {       
        print(enemyNum);
        print("Spawn point is at " + transform.position);

        spawnedObject = enemies[enemyNum].GetComponent<AiStats>() ? Instantiate(enemies[enemyNum], transform.position, transform.rotation) : throw new Exception("Trying to spawn object without AIStats Component");
        spawnedObject.GetComponent<AiStats>().OnDeath += Death; //Event call
    }

    void Death()
    {
        spawnState = SpawnerState.Psuedo_Dead;
    }

    void OnReload()
    {
        // These aren't being ran because the game only loads: dead, filled, or empty.
        // Maybe perform check on pseudos and not load them.
        // Can't ignore them.
        if (spawnState == SpawnerState.Psuedo_Dead)
        {
            Debug.Log("AAAAAA");
            spawnState = SpawnerState.Psuedo_Filled;
        }
        else if (spawnState == SpawnerState.Psuedo_Filled)
        {
            Debug.Log("AAAAAA");
            spawnState = SpawnerState.Empty;
            Destroy(spawnedObject);
        }
        //----------------------------------------------

        if (spawnedObject != null)
            spawnedObject.GetComponent<AiStats>().ResetAI();
    }

    void OnSave()
    {
        if (spawnState == SpawnerState.Psuedo_Dead)
        {
            spawnState = SpawnerState.Dead;
            Destroy(spawnedObject);
        }

        if (spawnState == SpawnerState.Psuedo_Filled)
        {
            spawnState = SpawnerState.Filled;
        }
    }

    public object CaptureState()
    {
        //print("State is " + (int) state);
        return new SaveData
        {

            spawnState = spawnState,
            enemyNum = enemyNum
        };
    }

    public void RestoreState(object state)
    {
        var saveData = (SaveData)state;

        spawnState = saveData.spawnState;
        enemyNum = saveData.enemyNum;
    }

    [Serializable]
    private struct SaveData
    {
        public SpawnerState spawnState;
        public int enemyNum;
    }
}
