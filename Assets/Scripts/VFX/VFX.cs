using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VFX : MonoBehaviour
{
    ParticleSystem rootParticles;
    ParticleSystem[] allParticles = null;

    public float duration = -1;

    public ParticleSystem[] AllParticles
    {
        get
        {
            if (allParticles == null)
            {
                allParticles = GetComponentsInChildren<ParticleSystem>();
            }
            return allParticles;
        }

    }

    // Use this for initialization
    void Awake()
    {
        Init();
    }

    // Update is called once per frame
    void Update()
    {
    }
    public void Play(float duration = -1)
    {
        rootParticles.Play(true);
    }

    public void Stop(bool stopInstant = false)
    {
        rootParticles.Stop(true, stopInstant ? ParticleSystemStopBehavior.StopEmittingAndClear : ParticleSystemStopBehavior.StopEmitting);
    }
    public void Init()
    {
        rootParticles = GetComponent<ParticleSystem>();
        Stop(true);
        if (duration > 0)
        {
            Destroy(this.gameObject, duration * 1.5f);
        }
    }
    public void SetColor(Color c)
    {
        foreach (ParticleSystem ps in AllParticles)
        {
            ParticleSystem.MainModule pm = ps.main;
            pm.startColor = c;
        }
    }
}
