using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IHpController
{
    public int MAX_HP { get; set; }
    public int currentHp { get; set; }

    public void Damage(int amount);

    public void Die();
}
