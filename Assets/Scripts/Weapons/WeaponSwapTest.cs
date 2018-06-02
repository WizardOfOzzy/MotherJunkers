using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSwapTest : MonoBehaviour 
{
    public WeaponController _weaponController;

	public void Update()
	{
        if (Input.GetKeyDown(KeyCode.A))
        {
            SwapWeapon();
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            AddNewRandomWeapon();
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            RemoveActiveWeapon();
        }
	}

	public void SwapWeapon()
    {
        _weaponController.SwapWeapons();
        Debug.Log("Swapped Weapons");
    }

    public void AddNewRandomWeapon()
    {
        GameObject prefab = WeaponFactory.Instance.GetRandomWeapon();
        GameObject instance = Instantiate(prefab);
        _weaponController.AttachWeapon(instance.GetComponent<Weapon>());
        Debug.Log("Attached New Random Weapon");
    }

    public void RemoveActiveWeapon()
    {
        _weaponController.DestroyActiveWeapon();
        Debug.Log("Removed Active Weapon");
    }
}
