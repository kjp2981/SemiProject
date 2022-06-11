using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    #region Singleton
    private UIManager instance;
    public UIManager Instance
    {
        get
        {
            if(instance == null)
            {
                instance = this;
            }
            return instance;
        }
    }
    #endregion

    //[SerializeField]
    //private 

    void Start()
    {
        
    }

    void Update()
    {
        
    }
}
