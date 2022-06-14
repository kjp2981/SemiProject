using System.Collections;
using System.Collections.Generic;
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

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void NexusHpbarValue(IHpController hpController)
    {
        nexusHpbar.fillAmount = (float)hpController.currentHp / (float)hpController.MAX_HP;
    }
}
