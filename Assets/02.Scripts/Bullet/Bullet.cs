using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : PoolableMono
{
    [SerializeField]
    private float speed = 10f;
    [SerializeField]
    private float life = 5f;

    RaycastHit hit;
    public LayerMask hitLayerMask;

    private void OnEnable()
    {
        StartCoroutine(DestroyBulletCoroutine());
    }

    private void Update()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);

        Physics.Raycast(transform.position, transform.forward, out hit, .2f, hitLayerMask);
        Debug.DrawRay(transform.position, transform.forward * .2f, Color.green);
    }

    private IEnumerator DestroyBulletCoroutine()
    {
        yield return new WaitForSeconds(life);
        PoolManager.Instance.Push(this);
    }

    private void OnTriggerEnter(Collider other)
    {
        // 타격 이펙트 생성
        PoolManager.Instance.Push(this);
    }

    public override void Reset()
    {
        
    }
}
