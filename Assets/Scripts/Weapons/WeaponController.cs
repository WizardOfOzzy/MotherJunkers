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
    private GameObject _machineGunPrefab;

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

        // Disable active weapon, enable and set special weapon active
        if (_activeWeaponIdx == 0)
        {
            DisableWeapon(_activeWeaponIdx);
        }
        _activeWeaponIdx = 1;
        EnableWeapon(_activeWeaponIdx);
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
        _weapons[0] = Instantiate(_machineGunPrefab).GetComponent<Weapon>();
        ParentWeaponGameObject(_weapons[0].gameObject);
        EnableWeapon(0);
    }

    private void ParentWeaponGameObject(GameObject go)
    {
        go.transform.parent = _weaponAttach;
        go.transform.localPosition = Vector3.zero;
    }

    private void DestroyWeapon(Weapon weapon)
    {
        weapon.enabled = false;
        Destroy(weapon.gameObject);
    }
}