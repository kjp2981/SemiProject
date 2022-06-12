using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterPair
{
    public GameObject prefab;
    public int count;
}

[CreateAssetMenu(menuName = "SO/System/EnemySpawnList")]
public class MonsterSpawnSO : ScriptableObject
{
    public List<List<MonsterPair>> monsterSpawnList;
}
