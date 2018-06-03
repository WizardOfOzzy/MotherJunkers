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
    }
    public override void OnFirePressed()
    {
        base.OnFirePressed();
        TryFireWeapon();
        timeTrack = ammoUseRatePer;
        if (CurrentAmmo > 0)
            flameVFX.Play();
    }
    public override void OnFireReleased()
    {
        base.OnFireReleased();
        Stop();
    }
    public override void OnFireHeld()
    {
        base.OnFireHeld();
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
    public void Stop()
    {
        flameVFX.Stop();
    }
}
