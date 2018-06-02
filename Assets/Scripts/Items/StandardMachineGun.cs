using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StandardMachineGun : Weapon {
    float fireRate;
    float fireTrack;
    float offset = .5f;
    protected override void Start()
    {
        base.Start();
        fireRate = 1 / ShotsPerSecond ;
        fireTrack = fireRate;
    }
    protected override void FireWeapon()
    {
        //base.FireWeapon();
        MachineGunPool.Instance.SpawnProjectile(transform.position + Random.onUnitSphere * offset, transform.rotation);
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
            fireTrack -= Time.deltaTime;
        
        if (Input.GetKey(KeyCode.Space))
        {
            TryFireWeapon();
        }
    }
}
