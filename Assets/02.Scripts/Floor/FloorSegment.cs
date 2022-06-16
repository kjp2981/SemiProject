using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorSegment : MonoBehaviour
{
    private float timer = 0f;

    private bool isUp = false;

    [SerializeField]
    private float stageEnterTime = 1f;

    private ParticleSystem magicCircle;

    void Start()
    {
        magicCircle = GetComponentInChildren<ParticleSystem>();

        isUp = false;
    }

    void Update()
    {
        if (isUp)
            timer += Time.deltaTime;

        if(timer >= stageEnterTime)
        {
            magicCircle.Play();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            isUp = true;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            isUp = false;
        }
    }
}
