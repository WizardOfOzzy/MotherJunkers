using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    [SerializeField]
    private Transform _weaponAttach;

    private Weapon[] _weapons;
    private const int MAX_WEAPONS = 2;

    [SerializeField]
    [Range(0, MAX_WEAPONS - 1)]
    private int _activeWeaponIdx;

    public void AttachWeapon(Weapon weapon)
    {
        if (_specialWeapon != null)
        {
            DestroyWeapon(_specialWeapon);
        }

        _specialWeapon = weapon;
        ParentWeaponGameObject(weapon.gameObject);
    }

    public void DestroyActiveWeapon()
    {
        if (_specialWeapon != null)
        {
            DestroyWeapon(_specialWeapon);
        }
    }

    public void FireWeapon()
    {
        _activeWeapon.TryFireWeapon();
    }

    public void SwapWeapons(bool next = true)
    {
        int iter = next ? 1 : -1;
        int nextWeapon = _activeWeaponIdx + iter;

        if (nextWeapon < 0)
        {
            nextWeapon = MAX_WEAPONS - 1;
        }
        else if (nextWeapon >= MAX_WEAPONS)
        {
            nextWeapon = 0;
        }

        if (nextWeapon != _activeWeaponIdx)
        {
            DisableWeapon(_activeWeaponIdx);
            EnableWeapon(nextWeapon);
            _activeWeaponIdx = nextWeapon;
        }
    }

    private void EnableWeapon(int weaponIdx)
    {
        _weapons[weaponIdx].gameObject.SetActive(true);
    }

    private void DisableWeapon(int weaponIdx)
    {
        _weapons[weaponIdx].gameObject.SetActive(false);
    }

    private Weapon _activeWeapon
    {
        get
        {
            return _weapons[_activeWeaponIdx];
        }
    }

    // CBO - this can be null! Convenience method
    private Weapon _specialWeapon
    {
        get
        {
            return _weapons[1];
        }
        set
        {
            _weapons[1] = value;
        }
    }

    private void Awake()
    {
        _weapons = new Weapon[MAX_WEAPONS];
    }

    private void Start()
    {
        InitMachineGun();
    }

    private void InitMachineGun()
    {
        GameObject machineGunGO = WeaponFactory.Instance.GetMachineGun();
        _weapons[0] = Instantiate(machineGunGO).GetComponent<Weapon>();
        ParentWeaponGameObject(_weapons[0].gameObject);
        EnableWeapon(0);
    }

    private void ParentWeaponGameObject(GameObject go)
    {
        go.transform.parent = _weaponAttach;
    }

    private void DestroyWeapon(Weapon weapon)
    {
        weapon.enabled = false;
        Destroy(weapon.gameObject);
    }
}