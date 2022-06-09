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

    public bool isThoungh; // ������ �Ѿ�����?
    public bool iskonck; // �˹��� �ִ���?
    [Range(0f, 10f)]
    public float knockbackPower;

    public bool isInfinite; // �Ѿ� �������ΰ�?
    [Range(5, 100)]
    public int bulletCount; // �ѹ��� ���� �� �ִ� �Ѿ� ��
}
