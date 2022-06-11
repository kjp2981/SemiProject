using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieController : Monster
{
    public override void Damage(int amount)
    {
        base.Damage(amount);
    }

    protected override void Attack()
    {
        base.Attack();
    }
}
