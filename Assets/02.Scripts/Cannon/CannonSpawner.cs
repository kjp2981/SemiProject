using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonSpawner : MonoBehaviour
{
    #region Singleton
    private static CannonSpawner instance = null;
    public static CannonSpawner Instance
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

    void Awake()
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

    public void SpawnCannon(Transform pos)
    {
        Cannon cannon = PoolManager.Instance.Pop("Cannon") as Cannon;
        cannon.transform.position = pos.position;
        GoldManager.Instance.SubtractGold(300);
    }
}
