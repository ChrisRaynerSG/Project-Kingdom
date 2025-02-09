using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileDetailObjectPool : MonoBehaviour
{

    public static TileDetailObjectPool Instance { get; private set; }

    public GameObject tileDetailPrefab;
    public int poolSize = 2500;
    public bool canGrow = true;

    private Queue<GameObject> pool;

    [SerializeField] private Transform poolContainer;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        
        else
        {
            Destroy(gameObject);
            return;
        }

        pool = new Queue<GameObject>();
        StartCoroutine(InitializePool());
    }

    public GameObject GetObject(Vector3 position, Transform parent)
    {
        if(pool.Count > 0)
        {
            GameObject obj = pool.Dequeue();
            if(!obj.activeInHierarchy)
            {
                obj.transform.position = position;
                obj.transform.SetParent(parent);
                obj.SetActive(true);
                return obj;
            }
            else
            {
                pool.Enqueue(obj);
                return GetObject(position, parent);
            }
        }

        if (canGrow)
        {
            GameObject obj = Instantiate(tileDetailPrefab, position, Quaternion.identity, parent);
            return obj;
        }

        return null;
    }

    public void ReturnObject(GameObject obj)
    {
        obj.GetComponent<TileDetailController>().ResetData();
        obj.SetActive(false);
        // obj.transform.SetParent(poolContainer);
        pool.Enqueue(obj);
    }

    private IEnumerator InitializePool()
    {

        for (int i = 0; i < poolSize; i++)
        {
            GameObject tile = Instantiate(tileDetailPrefab, poolContainer);
            tile.SetActive(false);
            pool.Enqueue(tile);
            
            if(i % 100 == 0){ // Yield every 100 objects to avoid freezing the game, I love coroutines, could even add a loading bar with this and an event system,
                yield return null;
            }
        }
    }
}