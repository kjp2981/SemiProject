using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SO/Creature/EnemyData")]
public class MonsterInfoSO : ScriptableObject
{
    [Range(1f, 10f)]
    public float speed;
    [Range(10, 100)]
    public int maxHp;
    public float attackDamage;
    public float attackDelay;
    public int goldAmount; // 처치시 얻는 골드의 양
}
