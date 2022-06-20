using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SO/Weapon/Cannon")]
public class CannonDataSO : ScriptableObject
{
    public int damage;
    public float delay;
    public GameObject buletPrefab;
    public float radius;
}
