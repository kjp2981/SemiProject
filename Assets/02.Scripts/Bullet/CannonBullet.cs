using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonBullet : Bullet
{
    private new SphereCollider collider;

    [SerializeField]
    private float hitRadius = .6f;
    [SerializeField]
    private LayerMask hitLayer;

    Collider[] cols;

    protected override void Awake()
    {
        base.Awake();
        collider = transform.GetComponentInChildren<SphereCollider>();
        collider.radius = hitRadius;
    }

    protected override void Update()
    {
        base.Update();
        cols = Physics.OverlapSphere(transform.position, hitRadius, hitLayer);

        if(cols.Length > 0)
        {
            foreach(Collider c in cols)
            {
                c.GetComponent<IHpController>().Damage(10);
                Debug.Log($"hit to {c.gameObject.name}");
                // TODO : 气惯 捞棋飘 积己
            }
            BloodImpact impact = PoolManager.Instance.Pop("CannonBulletExplosionImpact") as BloodImpact;
            impact.transform.position = transform.position;
            PoolManager.Instance.Push(this);
        }
    }

    //private void OnTriggerEnter(Collider other)
    //{
    //    Debug.Log("Hit!");

    //    if(other.gameObject.layer == hitLayer)
    //    {
    //        other.GetComponent<IHpController>().Damage(10);
    //        Debug.Log("Enemy hit");
    //        // TODO : 气惯 捞棋飘 积己
    //        BloodImpact impact = PoolManager.Instance.Pop("CannonBulletExplosionImpact") as BloodImpact;
    //        impact.transform.position = transform.position;
    //        PoolManager.Instance.Push(this);
    //    }
    //}

#if UNITY_EDITOR
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, hitRadius);
        Gizmos.color = Color.white;
    }
#endif
}
