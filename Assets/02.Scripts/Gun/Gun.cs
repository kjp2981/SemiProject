using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    [SerializeField] private GunInfoSO gunData;
    public GunInfoSO GunData { get => gunData; }
}
