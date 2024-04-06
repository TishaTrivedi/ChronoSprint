using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPooler : MonoBehaviour
{
    public GameObject[] pooledObjects;
    public int[] pooledAmounts;

    private List<GameObject>[] pools;

    void Start()
    {
        pools = new List<GameObject>[pooledObjects.Length];

        for (int i = 0; i < pooledObjects.Length; i++)
        {
            pools[i] = new List<GameObject>();

            for (int j = 0; j < pooledAmounts[i]; j++)
            {
                GameObject obj = Instantiate(pooledObjects[i]);
                obj.SetActive(true);
                pools[i].Add(obj);
            }
        }
    }

    public GameObject GetPooledObject(int poolIndex)
    {
        for (int i = 0; i < pools[poolIndex].Count; i++)
        {
            if (!pools[poolIndex][i].activeInHierarchy)
            {
                pools[poolIndex][i].SetActive(true);
                return pools[poolIndex][i];
            }
        }

        GameObject obj = Instantiate(pooledObjects[poolIndex]);
        obj.SetActive(true);
        pools[poolIndex].Add(obj);
        return obj;
    }
}
