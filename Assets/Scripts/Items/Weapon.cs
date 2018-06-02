using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : Item 
{
    public string Name;

    private const float MIN_AMMO = 0f;

    [SerializeField]
    private float CurrentAmmo, MaxAmmo, AmmoPerShot, ShotsPerSecond;

    public bool HasInfiniteAmmo;

    [SerializeField]
    private GameObject ProjectilePrefab;

    // Returns true if the weapon successfully fires
    public bool TryFireWeapon()
    {
        bool didFire = false;

        // Check to see if we have enough ammo to fire
        if (HasInfiniteAmmo)
        {
            didFire = true;
        }
        else if (CurrentAmmo > MIN_AMMO)
        {
            // Always allow 
            CurrentAmmo = Mathf.Max(MIN_AMMO, CurrentAmmo - AmmoPerShot);
            didFire = true;
        }

        // Actually fire the weapon
        if (didFire)
        {
            FireWeapon();
        }

        return didFire;
    }

    private void FireWeapon()
    {
        // TODO - spawn projecticle prefab, have it do things
    }
}
