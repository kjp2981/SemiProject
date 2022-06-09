using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleImpact : PoolableMono
{
    private ParticleSystem particle;

    public override void Reset()
    {
        
    }

    private void OnEnable()
    {
        if (particle == null)
            particle = GetComponent<ParticleSystem>();

        particle.Play();
    }

    void Awake()
    {
        particle = GetComponent<ParticleSystem>();
    }

    private void OnParticleSystemStopped()
    {
        PoolManager.Instance.Push(this);
    }
}
