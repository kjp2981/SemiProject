using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SO/Weapon/Bullet")]
public class BulletInfoSO : ScriptableObject
{
    public string bulletName;
    public Sprite bulletImage;

    public float speed;
    public float life;

    public GameObject obstacleImpact;
    public GameObject enemyImpact;
}
