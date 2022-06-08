using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private GameManager instance = null;
    public GameManager Instance
    {
        get
        {
            if(instance == null)
            {
                instance = this;
            }
            return instance;
        }
    }

    [SerializeField] private PoolingList poolingList;

    private void Awake()
    {
        instance = this;

        PoolManager.Instance = new PoolManager(transform);

        CreatePool();
    }

    void CreatePool()
    {
        foreach (PoolingPair pair in poolingList.list)
        {
            PoolManager.Instance.CreatePool(pair.prefab, pair.count);
        }
    }
}
