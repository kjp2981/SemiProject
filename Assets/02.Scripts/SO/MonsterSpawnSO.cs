using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class MonsterPair
{
    public GameObject prefab;
    public int count;
}

[CreateAssetMenu(menuName = "SO/System/EnemySpawnList")]
public class MonsterSpawnSO : ScriptableObject
{
    public List<MonsterPair> monsterSpawnList;
}
