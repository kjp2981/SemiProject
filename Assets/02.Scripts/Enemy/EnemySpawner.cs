using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class EnemySpawner : MonoBehaviour
{
    [SerializeField]
    private MonsterSpawnSO spawnData;

    public Dictionary<int, List<MonsterPair>> spawnDictionary = new Dictionary<int, List<MonsterPair>>();

    void Awake()
    {
        for(int i = 0; i < spawnData.monsterSpawnList.Count; i++)
        {
            spawnDictionary.Add(i, spawnData.monsterSpawnList[i]);
        }

        //foreach(KeyValuePair<int, List<MonsterPair>> pair in spawnDictionary)
        //{
        //    Debug.Log($"{pair.Key}, {pair.Value[1].count}, {pair.Value[1].prefab}");
        //}
    }

    void Update()
    {
        
    }
}
