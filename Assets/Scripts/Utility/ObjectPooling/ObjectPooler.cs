using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPooler : MonoBehaviour
{

    [System.Serializable]
    public class Pool
    {
        public string tag;
        public GameObject prefab;
        public int size;
    }

    public List<Pool> pools;
    public Dictionary<string, Queue<GameObject>> poolDictionary;

    #region Singleton
    public static ObjectPooler instance;

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
        RunObjectPooler();
    }

    public void RunObjectPooler()
    {
        poolDictionary = new Dictionary<string, Queue<GameObject>>();

        foreach (Pool pool in pools)
        {
            Queue<GameObject> objectPool = new Queue<GameObject>();

            for (int i = 0; i < pool.size; i++)
            {
                GameObject obj = Instantiate(pool.prefab);
                obj.SetActive(false);
                objectPool.Enqueue(obj);
            }

            poolDictionary.Add(pool.tag, objectPool);
        }
    }

    public GameObject SpawnFromPool(string tag, Vector3 pos, Quaternion rot)
    {
        if (!poolDictionary.ContainsKey(tag))
        {
            Debug.LogWarning("Pool with tag " + tag + " does not exist!");
            return null;
        }

        GameObject objectToSpawn = poolDictionary[tag].Dequeue();

        objectToSpawn.SetActive(true);

        objectToSpawn.transform.position = pos;
        objectToSpawn.transform.rotation = rot;

        IPooledObject pooledObj = objectToSpawn.GetComponent<IPooledObject>();

        if (pooledObj != null)
        {
            pooledObj.OnObjectSpawn();
        }

        poolDictionary[tag].Enqueue(objectToSpawn);

        return objectToSpawn;
    }

    public void Despawn(GameObject obj)
    {
        obj.SetActive(false);
    }

    public IEnumerator Despawn(GameObject obj, float time)
    {
        float startTime = 0;

        while (startTime < time)
        {
            if (!obj.tag.Equals("Dash"))
            {
                while (GameManager.stoppedTime)
                {
                    yield return new WaitUntil(() => GameManager.stoppedTime);
                }
            }

            startTime += Time.deltaTime;
            yield return null;          
        }

        obj.SetActive(false);
        //yield return new WaitForSeconds(time);       
    }

    public GameObject GetObject(string tag)
    {
        GameObject objectToSpawn = poolDictionary[tag].Dequeue();
        return objectToSpawn;
    }
}










