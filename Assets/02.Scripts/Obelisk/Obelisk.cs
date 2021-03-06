using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obelisk : MonoBehaviour, IHpController
{
    [SerializeField]
    private int maxHP;

    public int MAX_HP { get; set; }
    public int currentHp { get; set; }

    private Animation animation;

    void Start()
    {
        animation = GetComponent<Animation>();
        animation.CrossFade("Idle", 0.25f);

        MAX_HP = maxHP;
        currentHp = MAX_HP;
    }

    public void Damage(int amount)
    {
        currentHp -= amount;
        UIManager.Instance.NexusHpbarValue(GetComponent<IHpController>());

        if (currentHp <= 0)
        {
            Die();
        }
    }

    public void Die()
    {
        Debug.Log("Obelisk is Destroy");

        SceneMoveManager.Instance.MoveScene("Dead");
        // TODO : ?ؼ??? ?ı? ????Ʈ ?ֱ?
        //          ???? UI ?ٿ???
        //          ???? ??????
        //          ???? ?? ???߰? ???ֱ?
    }
}
