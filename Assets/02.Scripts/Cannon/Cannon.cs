using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cannon : MonoBehaviour
{
    [SerializeField]
    private float radius = 5f;
    [SerializeField]
    private LayerMask hitLayer;

    private Collider[] coll;

    private GameObject target;

    void Start()
    {
        
    }

    void Update()
    {
        coll = Physics.OverlapSphere(transform.position, radius, hitLayer);
        
        if(coll != null)
        {
            target = coll[0].gameObject;

            Attack();
        }
    }

    private void Attack()
    {
        
    }

#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, radius);
        Gizmos.color = Color.white;
    }
#endif
}
