using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Define;


public class EnemySpawner : MonoBehaviour
{
    #region Singleton
    private static EnemySpawner instance = null;
    public static EnemySpawner Instance
    {
        get
        {
            if (instance == null)
            {
                instance = null;
            }
            return instance;
        }
    }
    #endregion

    [SerializeField]
    private MonsterSpawnSO[] spawnData;

    public Dictionary<int, List<MonsterPair>> spawnDictionary = new Dictionary<int, List<MonsterPair>>();

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }

        for (int i = 0; i < spawnData.Length; i++)
        {
            spawnDictionary.Add(i + 1, spawnData[i].monsterSpawnList);
        }

        //foreach(KeyValuePair<int, List<MonsterPair>> pair in spawnDictionary)
        //{
        //    Debug.Log($"{pair.Key}, {pair.Value[1].count}, {pair.Value[1].prefab}");
        //}
    }

    public IEnumerator SpawnEnemy(int stage)
    {
        for(int i = 0; i < spawnData[stage].monsterSpawnList.Count; i++)
        {
            yield return new WaitForSeconds(1f);
            for(int j = 0; j < spawnData[stage].monsterSpawnList[i].count; j++)
            {
                yield return new WaitForSeconds(.3f);
                Monster monster = PoolManager.Instance.Pop(spawnData[stage].monsterSpawnList[i].prefab.name) as Monster;
                monster.transform.position = EnemySpawnPos.position;
            }
        }

        foreach(KeyValuePair<int, List<MonsterPair>> pair in spawnDictionary)
        {
            if(stage == pair.Key)
            {
                for(int i = 0; i < pair.Value.Count; i++)
                {
                    for(int j = 0; j < pair.Value[i].count; j++)
                    {
                        yield return new WaitForSeconds(.3f);
                        Monster monster = PoolManager.Instance.Pop(pair.Value[i].prefab.name) as Monster;
                        monster.transform.position = EnemySpawnPos.position;
                    }
                }
            }
        }
    }
}
