using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MuzzleImpact : PoolableMono
{
    private ParticleSystem particle;
    private float duration;

    private void Awake()
    {
        particle = GetComponentInChildren<ParticleSystem>();
    }

    private void OnEnable()
    {
        if (particle == null)
            particle = GetComponentInChildren<ParticleSystem>();
        particle.Play();
        StartCoroutine(DestroyCoroutine());
    }

    private void OnParticleSystemStopped()
    {
        PoolManager.Instance.Push(this);
    }

    public override void Reset()
    {
        
    }

    IEnumerator DestroyCoroutine()
    {
        yield return new WaitForSeconds(1);
        PoolManager.Instance.Push(this);
    }
}
