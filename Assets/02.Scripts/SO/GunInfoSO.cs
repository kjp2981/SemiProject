using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SO/Weapon/Gun")]
public class GunInfoSO : ScriptableObject
{
    public string gunName;
    public Sprite gunImage;

    public GameObject gunPrefab;
    public float delay;
    public float damage;
    public RuntimeAnimatorController animator;
    public GameObject bulletPrefab;

    public bool isThoungh; // 관통형 총알인지?
    public bool iskonck; // 넉백이 있는지?
    [Range(0f, 5f)]
    public float knockbackPower;

    [Range(5, 100)]
    public int bulletCount; // 한번에 가질 수 있는 총알 수
    public float reloadDelay;
}
