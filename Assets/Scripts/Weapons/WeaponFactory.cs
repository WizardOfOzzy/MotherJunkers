using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponFactory : MonoBehaviour 
{
    public static WeaponFactory Instance
    {
        get
        {
            return _instance;
        }
    }
    private static WeaponFactory _instance;

    [SerializeField]
    private GameObject _machineGunPrefab;

    [SerializeField]
    private GameObject[] _weaponPrefabs;

    public GameObject GetMachineGun()
    {
        return _machineGunPrefab;
    }

    public GameObject GetRandomWeapon()
    {
        if (_weaponPrefabs == null || _weaponPrefabs.Length == 0)
        {
            Debug.LogError("Weapon prefabs need to be added to the WeaponFactory!");
        }

        int rand = Random.Range(0, _weaponPrefabs.Length - 1);
        return _weaponPrefabs[rand];
    }

    private void Awake()
    {
        if (_instance != null)
        {
            Debug.LogError("There should only be one instance of WeaponFactory in the scene.");
        }
        _instance = this;
    }
}
