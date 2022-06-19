using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReloadImage : MonoBehaviour
{
    [SerializeField]
    private float rotSpeed = 3f;

    void Update()
    {
        transform.Rotate(Vector3.forward * Time.deltaTime * -rotSpeed);
    }
}
