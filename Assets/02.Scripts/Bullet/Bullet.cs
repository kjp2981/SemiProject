using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : PoolableMono
{
    [SerializeField]
    private BulletInfoSO bulletSO;
    public BulletInfoSO BulletSO { get => bulletSO; }

    private TrailRenderer trailRenderer;
    private int damage = 1;

    private void Awake()
    {
        trailRenderer = GetComponent<TrailRenderer>();
    }

    private void OnEnable()
    {
        StartCoroutine(DestroyBulletCoroutine());
    }

    private void Update()
    {
        transform.Translate(Vector3.forward * bulletSO.speed * Time.deltaTime);
    }

    public void SetDamage(int damage)
    {
        this.damage = damage;
    }

    private IEnumerator DestroyBulletCoroutine()
    {
        yield return new WaitForSeconds(bulletSO.life);
        PoolManager.Instance.Push(this);
    }

    private void OnCollisionEnter(Collision collider)
    {
        // 타격 이펙트 생성
        if (collider.gameObject.layer == LayerMask.NameToLayer("ENEMY"))
        {
            BloodImpact impact = PoolManager.Instance.Pop(bulletSO.enemyImpact.name) as BloodImpact;
            Quaternion rot = Quaternion.LookRotation(collider.GetContact(0).normal);
            impact.transform.SetPositionAndRotation(collider.GetContact(0).point, rot);

            collider.gameObject.GetComponent<IHpController>().Damage(damage);
            StopAllCoroutines();
            PoolManager.Instance.Push(this);
            //collider.gameObject.GetComponent<Monster>().Knockback() // 넉백인데 추후 구현
        }
        else if (collider.gameObject.layer == LayerMask.NameToLayer("OBSTACLE"))
        {
            ObstacleImpact impact = PoolManager.Instance.Pop(bulletSO.obstacleImpact.name) as ObstacleImpact;
            Quaternion rot = Quaternion.LookRotation(-collider.GetContact(0).normal);
            impact.transform.SetPositionAndRotation(collider.GetContact(0).point, rot);
            StopAllCoroutines();
            PoolManager.Instance.Push(this);
        }
        else
        {
            StopAllCoroutines();
            PoolManager.Instance.Push(this);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("HANDLE"))
        {
            other.GetComponent<Handle>().HandleAnim();
            PoolManager.Instance.Push(this);
        }
    }

    public override void Reset()
    {
        transform.rotation = Quaternion.Euler(Vector3.zero);
        trailRenderer.Clear();
    }
}
