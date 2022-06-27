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
            UIManager.Instance.SetGoldText(gold);
            //GoldPopupText();
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

        Gold = 500;
    }

    public void AddGold(int amount)
    {
        Gold += amount;
    }

    public void SubtractGold(int amount)
    {
        Gold -= amount;
    }

    private void GoldPopupText()
    {
        TextPopup text = PoolManager.Instance.Pop("PopupText") as TextPopup;
        //text.transform.SetParent(Canvas.transform.position);
        text.transform.SetPositionAndRotation(UIManager.Instance.GoldText.transform.localPosition, Quaternion.identity);
        text.Popup($"+ {Gold}", new Color(255, 201, 51));
    }
}
