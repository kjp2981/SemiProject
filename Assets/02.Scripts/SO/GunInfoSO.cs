using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SO/Weapon/Gun")]
public class GunInfoSO : ScriptableObject
{
    public GameObject gunPrefab;
    public float delay;
    public float damage;
    public RuntimeAnimatorController animator;
    public GameObject bulletPrefab;

    public bool isThoungh; // 관통형 총알인지?
    public bool iskonck; // 넉백이 있는지?
    [Range(0f, 10f)]
    public float knockbackPower;

    public bool isInfinite; // 총알 무제한인가?
    [Range(5, 100)]
    public int bulletCount; // 한번에 가질 수 있는 총알 수
}
