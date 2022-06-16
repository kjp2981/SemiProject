using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IKnockback
{
    public void Knockback(float knockbackPower, Vector3 direction);
}