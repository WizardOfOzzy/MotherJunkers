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

    // TODO - get this from a separate component
    public int _playerIndex = 0;



    public void AttachWeapon(Weapon weapon)
    {
        weapon._playerIndex = _playerIndex;
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
            if (_activeWeaponIdx != 0)
            {
                _activeWeaponIdx = 0;
                EnableWeapon(_activeWeaponIdx);
            }

            DestroyWeapon(_specialWeapon);
        }
    }

    public void FireWeapon()
    {
        bool didFire = _activeWeapon.TryFireWeapon();
        if (didFire)
        {
            Publisher.Raise(new WeaponFiredEvent(_playerIndex, _activeWeapon));
        }
    }

    public void SwapWeapons(bool next = true)
    {
        if (_specialWeapon != null)
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
    }

    private void EnableWeapon(int weaponIdx)
    {
        _weapons[weaponIdx].gameObject.SetActive(true);
        Publisher.Raise(new WeaponChangedEvent(_playerIndex, _activeWeapon));
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
    float prevAxisValue;
    float axisThreshold = .25f;
    private void Awake()
    {
        _weapons = new Weapon[MAX_WEAPONS];
    }

    private void Start()
    {
        InitMachineGun();
    }
    public void Update()
    {
        float currentAxisValue = Mathf.Abs(PlayerInput.Instance.GetAxis((EController)_playerIndex+1, EControllerAxis.Trigger));

        if (prevAxisValue < axisThreshold && currentAxisValue >= axisThreshold)
        {
            OnFirePressed();
        }
        else if (prevAxisValue >= axisThreshold && currentAxisValue >= axisThreshold)
        {
            OnFireHeld();
        }
        else if (prevAxisValue >= axisThreshold && currentAxisValue < axisThreshold)
        {
            OnFireReleased();
        }
        prevAxisValue = currentAxisValue;
    }

    public void OnFirePressed()
    {
        _activeWeapon.OnFirePressed();
        }
    public void OnFireHeld()
    {
        _activeWeapon.OnFireHeld();
    }
    public void OnFireReleased()
    {
        _activeWeapon.OnFireReleased();
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