using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : Weapon
{
    public enum LASERSTATE
    {
        NONE,
        CHARGING,
        FIRING,
        COOLDOWN
    }
    public VFX charging, firing;
    public LASERSTATE state;
    public float chargeTime, cooldownTime, firingTime;
    float timeTracking = 0;
    private readonly Gradient _gradient = new Gradient { mode = GradientMode.Blend };
    public float damagePerSecond=30;
    protected override void Start()
    {
        base.Start();
        GradientColorKey[] colorKeys = new GradientColorKey[4];
        colorKeys[0] = new GradientColorKey(Color.white, 0.0f);
        colorKeys[1] = new GradientColorKey(Color.yellow, 0.0f);
        colorKeys[2] = new GradientColorKey(new Color(1, 127 / 255.0f, 80 / 255.0f), 0.5f);
        colorKeys[3] = new GradientColorKey(Color.red, 1.0f);

        GradientAlphaKey[] alphaKeys = new GradientAlphaKey[4];
        for (int i = 0; i < alphaKeys.Length; i++)
        {
            alphaKeys[i] = new GradientAlphaKey(1.0f, 0.0f);
        }

        _gradient.SetKeys(colorKeys, alphaKeys);
    }

    // Update is called once per frame
    void Update()
    {
        switch (state)
        {
            case LASERSTATE.NONE:

                break;
            case LASERSTATE.CHARGING:
                timeTracking -= Time.deltaTime;

                Color c;
                if (timeTracking <= 0)
                    c = Color.blue;
                else
                    c = _gradient.Evaluate(1 - (timeTracking / chargeTime));
                charging.SetColor(c);
                break;
            case LASERSTATE.FIRING:
                timeTracking -= Time.deltaTime;
                if (timeTracking <= 0)
                {
                    state = LASERSTATE.COOLDOWN;
                    timeTracking = cooldownTime;
                    firing.Stop(true);
                    SetDamageColliders(false);
                }
                break;
            case LASERSTATE.COOLDOWN:
                timeTracking -= Time.deltaTime;
                if (timeTracking <= 0)
                {
                    state = LASERSTATE.NONE;
                }
                break;
            default:
                break;
        }
    }
    protected override void FireWeapon()
    {
        base.FireWeapon();
        state = LASERSTATE.FIRING;
        firing.Play();
        timeTracking = firingTime;
        SetDamageColliders(true);
    }
    public override void OnFirePressed()
    {
        if (state != LASERSTATE.NONE) return;
        base.OnFirePressed();
        if (CurrentAmmo > 0)
        {
            state = LASERSTATE.CHARGING;
            charging.Play();
            timeTracking = chargeTime;
        }
    }

    public override void OnFireReleased()
    {
        if (state != LASERSTATE.CHARGING) return;
        base.OnFireReleased();

        if (timeTracking <= 0)
        {
            TryFireWeapon();
        }
        else
        {
            state = LASERSTATE.NONE;
            SetDamageColliders(false);
        }
        charging.Stop();
    }

    public override void OnSwapIn()
    {
        base.OnSwapIn();
        SetDamageColliders(false);
        state = LASERSTATE.NONE;
        charging.Stop(true);
        firing.Stop(true);
    }

    public override void OnSwapOut()
    {
        base.OnSwapOut();
        SetDamageColliders(false);
        state = LASERSTATE.NONE;
        charging.Stop(true);
        firing.Stop(true);
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
