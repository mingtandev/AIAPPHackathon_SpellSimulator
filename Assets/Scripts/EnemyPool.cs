﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class EnemyPool : MonoBehaviour
{
    // Start is called before the first frame update
    [System.Serializable]
    public class Pool
    {
        public int size;
        public GameObject prefab;
        public string tag;
    }

    public Dictionary<string, Queue<GameObject>> poolDictionary;

    public List<Pool> pools;

    //SINGLETON
    public static EnemyPool Instance;

    public Transform PosSpawn;

    private void Awake()
    {
        Instance = this;
    }

    void Start()
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

        StartCoroutine(ReSpawn());
    }

    public GameObject SpawnPool(string tag, Vector3 position, Quaternion rotation)
    {
        if (!poolDictionary.ContainsKey(tag))
        {
            Debug.LogWarning("Pool with tag " + tag + " doesn't exist");
            return null;
        }
        GameObject objectToSpawn = poolDictionary[tag].Dequeue();
        objectToSpawn.transform.position = position;
        objectToSpawn.transform.rotation = rotation;
        objectToSpawn.GetComponent<NavMeshAgent>().enabled = true;
        objectToSpawn.GetComponent<Enemy>().agent = objectToSpawn.GetComponent<NavMeshAgent>();
        objectToSpawn.GetComponent<Animator>().enabled = true;

        objectToSpawn.SetActive(true);
        poolDictionary[tag].Enqueue(objectToSpawn);

        return objectToSpawn;
    }

    IEnumerator ReSpawn()
    {
        yield return new WaitForSeconds(1f);
        EnemyPool.Instance.SpawnPool("Enemy", posSpawn().position, posSpawn().rotation);
        StartCoroutine(ReSpawn());
    }
    Transform posSpawn()
    {
        int lengthChild = PosSpawn.childCount;
        int ran = Random.Range(0, lengthChild);

        return PosSpawn.GetChild(ran).gameObject.transform;

    }


}


//GameObject bullet = BulletPool.Instance.SpawnPool("BulletTrail", ShotRay.origin, Quaternion.identity);
