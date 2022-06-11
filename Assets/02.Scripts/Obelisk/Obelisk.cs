using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obelisk : MonoBehaviour, IHpController
{
    [SerializeField]
    private int maxHP;

    public int MAX_HP { get; set; }
    public int currentHp { get; set; }

    void Start()
    {
        MAX_HP = maxHP;
        currentHp = MAX_HP;
    }

    public void Damage(int amount)
    {
        currentHp -= amount;

        if(currentHp <= 0)
        {
            Die();
        }
    }

    public void Die()
    {
        Debug.Log("Obelisk is Destroy");
        // TODO : ³Ø¼­½º ÆÄ±« ÀÌÆåÆ® ³Ö±â
        //          °ÔÀÓ UI ¶Ù¿ì±â µî...
    }
}
