using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Pool 
{
    public string tag;
    public GameObject prefab;
    public int size;
}

public class ObjectPooler : MonoBehaviour
{
    public static ObjectPooler Instance;
    public List<Pool> pools;
    private Dictionary<string, Queue<GameObject>> poolDictionary;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else 
        {
            Destroy(gameObject);
            return;
        }
        poolDictionary = new Dictionary<string, Queue<GameObject>>();

        foreach (Pool pool in pools)
        {
            Queue<GameObject> poolQueue = new Queue<GameObject>();

            for (int i = 0; i < pool.size; i++)
            {
                GameObject obj = Instantiate(pool.prefab);
                obj.transform.SetParent(transform, true);

                obj.SetActive(false);
                poolQueue.Enqueue(obj);
            }

            poolDictionary.Add(pool.tag, poolQueue);
        }
    }

     void Start()
    {
        foreach (var key in poolDictionary.Keys)
        {
            Debug.Log("Pool registrado: " + key);
        }
    }

    public GameObject SpawnFromPool(string tag, Vector3 position, Quaternion rotation) 
    {
        
        if (!poolDictionary.ContainsKey(tag)) 
        {
            Debug.LogWarning("Pool con tag" + tag + "no existe");
                return null;
        }
        GameObject objectToSpawn = poolDictionary[tag].Dequeue();

        objectToSpawn.SetActive(true);
        objectToSpawn.transform.position = position;
        objectToSpawn.transform.rotation = rotation;
        

        return objectToSpawn;
    }

    public void ReturnToPool(GameObject objectToReturn) 
    {
        objectToReturn.SetActive(false);
        poolDictionary[objectToReturn.tag].Enqueue(objectToReturn);
    }
}
