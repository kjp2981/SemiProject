using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SO/Creature/PlayerData")]
public class PlayerInfoSO : ScriptableObject
{
    public float moveSpeed;
    public float jumpPower;
    public int maxHp;
}
