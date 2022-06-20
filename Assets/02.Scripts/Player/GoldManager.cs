using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoldManager : MonoBehaviour
{
    #region Singleton
    private static GoldManager instance;
    public static GoldManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = null;
            }
            return instance;
        }
    }
    #endregion

    private int gold = 0;
    public int Gold
    {
        get => gold;
        private set
        {
            gold = value;
            // TODO : UI °»½Å
            UIManager.Instance.SetGoldText(gold);
        }
    }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    public void AddGold(int amount)
    {
        Gold += amount;
    }

    public void SubtractGold(int amount)
    {
        Gold -= amount;
    }
}
