using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VFX : MonoBehaviour {
    ParticleSystem rootParticles;
    public float duration = -1;
	// Use this for initialization
	void Start () {
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

    public void Stop()
    {
        rootParticles.Stop(true);
    }
    public void Init()
    {
        rootParticles = GetComponent<ParticleSystem>();
        Stop();
        if (duration > 0)
        {
            Destroy(this.gameObject, duration * 1.5f);
        }
    }
}
