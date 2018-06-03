using UnityEngine;

public class FlameThrower : Weapon
{
    public float ammoUseRatePer = 1;
    public VFX flameVFX;
    private float timeTrack;
    public float damagePerSecond = 15;
    protected override void FireWeapon()
    {
        base.FireWeapon();
    }

    public override void OnFirePressed()
    {
        base.OnFirePressed();
        TryFireWeapon();
        timeTrack = ammoUseRatePer;
        if (CurrentAmmo > 0)
        {
            SetDamageColliders(true);
            flameVFX.Play();
        }
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
        SetDamageColliders(false);
        flameVFX.Stop();
    }

    public override void OnSwapIn()
    {
        base.OnSwapIn();
        SetDamageColliders(false);
        Stop();
    }

    public override void OnSwapOut()
    {
        base.OnSwapOut();
        SetDamageColliders(false);
        Stop();
    }

    public void HandleCollision(GameObject g)
    {
        if (g.GetComponent<VehicleHealth>())
        {
            g.GetComponent<VehicleHealth>().TakeDamage(damagePerSecond * Time.fixedDeltaTime);
        }
    }
    private void OnTriggerStay(Collider other)
    {
        HandleCollision(other.gameObject);
    }
    private void OnTriggerEnter(Collider other)
    {
        HandleCollision(other.gameObject);
    }
}
