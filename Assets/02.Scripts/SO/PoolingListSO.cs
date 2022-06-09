using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PoolingPair
{
    public PoolableMono prefab;
    public int count;
}

[CreateAssetMenu(menuName = "SO/System/PoolinList")]
public class PoolingListSO : ScriptableObject
{
    public List<PoolingPair> list;
}
