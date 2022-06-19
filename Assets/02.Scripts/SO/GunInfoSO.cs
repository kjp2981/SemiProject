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

    public bool isThoungh; // ������ �Ѿ�����?
    public bool iskonck; // �˹��� �ִ���?
    [Range(0f, 5f)]
    public float knockbackPower;

    [Range(5, 100)]
    public int bulletCount; // �ѹ��� ���� �� �ִ� �Ѿ� ��
    public float reloadDelay;
}
