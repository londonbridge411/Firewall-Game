using System.Collections;
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

namespace bowen.Saving
{
    public class SaveLoadSystem : MonoBehaviour
    {
        #region Singleton
        public static SaveLoadSystem instance;

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

        private string SavePath => $"{Application.persistentDataPath}/save.txt";

        [ContextMenu("Save")]
        public void Save()
        {    
            var state = LoadFile();
            OnGameSave?.Invoke();
            CaptureState(state);
            SaveFile(state);       
        }

        [ContextMenu("Load")]
        public void Load()
        {
            var state = LoadFile();
            RestoreState(state);
            OnGameLoad?.Invoke();
        }

        private void SaveFile(object state)
        {
            using (var stream = File.Open(SavePath, FileMode.Create))
            {
                var formatter = new BinaryFormatter();
                formatter.Serialize(stream, state);
            }
        }

        private Dictionary<string, object> LoadFile()
        {
            if (!File.Exists(SavePath))
            {
                return new Dictionary<string, object>();
            }

            using (var stream = File.Open(SavePath, FileMode.Open))
            {
                var formatter = new BinaryFormatter();
                return (Dictionary<string, object>)formatter.Deserialize(stream);
            }
        }

        public void CaptureState(Dictionary<string, object> state)
        {
            foreach (var saveable in FindObjectsOfType<SaveableEntity>())
            {
                state[saveable.Id] = saveable.CaptureState();
            }
        }

        public void RestoreState(Dictionary<string, object> state)
        {
            List<EnemySpawner> ignoredSpawnersPsuedoFilled = new List<EnemySpawner>();
            List<EnemySpawner> ignoredSpawnersPsuedoDead = new List<EnemySpawner>();
            foreach (EnemySpawner e in FindObjectsOfType<EnemySpawner>())
            {
                if (e.spawnState == EnemySpawner.SpawnerState.Psuedo_Filled)
                {
                    ignoredSpawnersPsuedoFilled.Add(e);
                }

                if (e.spawnState == EnemySpawner.SpawnerState.Psuedo_Dead)
                {
                    ignoredSpawnersPsuedoDead.Add(e);
                }
            }

            foreach (var saveable in FindObjectsOfType<SaveableEntity>())
            {
                if (state.TryGetValue(saveable.Id, out object value))
                {
                    saveable.RestoreState(value);
                }
            }

            foreach (EnemySpawner e in ignoredSpawnersPsuedoFilled)
            {
                e.spawnState = EnemySpawner.SpawnerState.Psuedo_Filled;
            }

            foreach (EnemySpawner e in ignoredSpawnersPsuedoDead)
            {
                e.spawnState = EnemySpawner.SpawnerState.Psuedo_Dead;
            }
        }

        #region Saving & Loading Events
        public delegate void OnSave();
        public event OnSave OnGameSave;

        public delegate void OnLoad();
        public event OnLoad OnGameLoad;
        #endregion
    }
}

