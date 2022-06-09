using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MuzzleImpact : PoolableMono
{
    private ParticleSystem particle;

    private void Awake()
    {
        particle = GetComponent<ParticleSystem>();
    }

    private void OnEnable()
    {
        if (particle == null)
            particle = GetComponent<ParticleSystem>();
        particle.Play();
    }

    private void OnParticleSystemStopped()
    {
        PoolManager.Instance.Push(this);
    }

    public override void Reset()
    {
        
    }
}
