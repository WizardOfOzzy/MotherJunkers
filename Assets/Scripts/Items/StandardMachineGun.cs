using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StandardMachineGun : Weapon {
    float fireRate;
    float fireTrack;
    float offset = .25f;
    protected override void Start()
    {
        base.Start();
        fireRate = 1 / ShotsPerSecond ;
        fireTrack = -1;
    }
    protected override void FireWeapon()
    {
        //base.FireWeapon();
        MachineGunPool.Instance.SpawnProjectile(spawnPoint.position + Random.onUnitSphere * offset, spawnPoint.rotation);
        fireTrack = fireRate;
    }
    protected override bool CanFire()
    {
        bool canFire= base.CanFire();
        canFire = canFire && fireTrack <= 0;
        return canFire;
    }
    void Update()
    {
        if (fireTrack > 0)
        {
            fireTrack -= Time.deltaTime;
            if (!_audioSource.isPlaying)
            {
                Debug.Log("DERP");
                _audioSource.Play();
            }
        }
        else
        {
            _audioSource.Stop();
            _audioSource.time = 0;
        }
        if (Input.GetKey(KeyCode.Space))
        {
            TryFireWeapon();
        }
    }
}
