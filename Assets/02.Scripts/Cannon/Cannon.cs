using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Cannon : PoolableMono
{
    [SerializeField]
    private CannonDataSO cannonDataSO;

    [SerializeField]
    private LayerMask hitLayer;

    private Collider[] coll;
    private Collider targetCol;

    private Transform firePos;
    private Transform cannonBarrel;

    private float timer = 0f;

    private IEnumerator checkCoroutine;
    void Start()
    {
        firePos = transform.Find("cannon barrel/FirePos");
        cannonBarrel = transform.Find("cannon barrel");

        timer = cannonDataSO.delay;

        checkCoroutine = CheckTarget();
        StartCoroutine(checkCoroutine);
    }

    private void OnDisable()
    {
        StopAllCoroutines();
    }

    void Update()
    {
        timer += Time.deltaTime;

        coll = Physics.OverlapSphere(transform.position, cannonDataSO.radius, hitLayer);
        
        if(coll.Length > 0)
        {   
            Attack();
        }
    }

    IEnumerator CheckTarget()
    {
        WaitForSeconds ws = new WaitForSeconds(1f);
        while (true)
        {
            yield return ws;
            if (coll.Length > 0)
            {
                targetCol = coll[0];
                foreach (Collider col in coll)
                {
                    if (Vector3.Distance(transform.position, col.transform.position) < Vector3.Distance(transform.position, targetCol.transform.position))
                    {
                        targetCol = col;
                    }
                }
            }
        }
    }

    private void Attack()
    {
        if (targetCol != null)
        {
            cannonBarrel.LookAt(targetCol.transform);
            if (timer >= cannonDataSO.delay)
            {
                Debug.Log($"Attack, {targetCol}");
                Bullet bullet = PoolManager.Instance.Pop("CannonBullet") as Bullet;
                bullet.SetDamage(cannonDataSO.damage);
                bullet.transform.SetPositionAndRotation(firePos.position, firePos.rotation);
                timer = 0f;
            }
        }
    }

    public override void Reset()
    {
        
    }

#if UNITY_EDITOR
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, cannonDataSO.radius);
        Gizmos.color = Color.white;
    }
#endif
}
