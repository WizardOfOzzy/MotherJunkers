using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlameThrower : Weapon
{
    public float ammoUseRatePer = 1;
    public VFX flameVFX;
    float timeTrack;
    protected override void FireWeapon()
    {
        base.FireWeapon();

    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            TryFireWeapon();
            timeTrack = ammoUseRatePer;
            if (CurrentAmmo > 0)
                flameVFX.Play();
        }
        else if (Input.GetKey(KeyCode.Space))
        {
            if (CurrentAmmo <= 0)
            {
                Stop();
                return;
            }
            timeTrack -= Time.deltaTime;
            if (timeTrack <= 0)
            {
                TryFireWeapon();
                timeTrack = ammoUseRatePer;
            }

        }
        else if (Input.GetKeyUp(KeyCode.Space))
        {
            Stop();
        }
    }

    public void Stop()
    {
        flameVFX.Stop();
    }
}
