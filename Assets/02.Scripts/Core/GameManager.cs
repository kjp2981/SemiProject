using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    #region Singleton
    private static GameManager instance = null;
    public static GameManager Instance
    {
        get
        {
            if(instance == null)
            {
                instance = null;
            }
            return instance;
        }
    }
    #endregion

    [SerializeField] private PoolingListSO poolingList;

    private int stageCount = 0;

    private void Awake()
    {
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = false;

        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }


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

    public void StartStage(int stageNum)
    {
        StartCoroutine(EnemySpawner.Instance.SpawnEnemy(stageNum));
    }
}
