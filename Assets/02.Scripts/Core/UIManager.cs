using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    #region Singleton
    private static UIManager instance;
    public static UIManager Instance
    {
        get
        {
            if(instance == null)
            {
                instance = null;
            }
            return instance;
        }
    }
    #endregion

    [SerializeField]
    private Image nexusHpbar;
    [SerializeField]
    private Image playerHpbar;

    [SerializeField]
    private TextMeshProUGUI gunText;
    [SerializeField]
    private Image gunImage;

    [SerializeField]
    private Image realodImage;

    [SerializeField]
    private Image BulletImage;
    [SerializeField]
    private TextMeshProUGUI bulletCountText;

    [SerializeField]
    private TextMeshProUGUI goldText;
    public TextMeshProUGUI GoldText => goldText;
    [SerializeField]
    private TextMeshProUGUI cannonText;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    public void NexusHpbarValue(IHpController hpController)
    {
        nexusHpbar.fillAmount = (float)hpController.currentHp / (float)hpController.MAX_HP;
    }

    public void PlayerHpbarValue(IHpController hpController)
    {
        playerHpbar.fillAmount = (float)hpController.currentHp / (float)hpController.MAX_HP;
    }

    public void ChangeGunInfo(string name, Sprite sprite)
    {
        gunText.SetText(name);
        gunImage.sprite = sprite;
        gunImage.SetNativeSize();
    }

    public void SetReloadImageActive(bool isActive)
    {
        realodImage.gameObject.SetActive(isActive);
    }

    public void SetBulletCountAndImage(int amount, Sprite sprite = null)
    {
        bulletCountText.SetText(string.Format($"X   {amount}"));
        if (sprite != null)
        {
            BulletImage.sprite = sprite;
            BulletImage.SetNativeSize();
        }
    }

    public void SetGoldText(int amount)
    {
        goldText.SetText(string.Format($"GOLD : {amount}"));
    }

    public void SetCannonTextActive(bool value)
    {
        cannonText.gameObject.SetActive(value);
    }
}
