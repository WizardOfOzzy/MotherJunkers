using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSwapTest : MonoBehaviour 
{
    public WeaponController _weaponController;

	public void Start()
	{
        Publisher.Subscribe<WeaponChangedEvent>(OnWeaponChangedEvent);
        Publisher.Subscribe<WeaponFiredEvent>(OnWeaponFiredEvent);
	}

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
        else if (Input.GetKey(KeyCode.Space))
        {
            _weaponController.FireWeapon();
        }
	}

	public void SwapWeapon()
    {
        _weaponController.SwapWeapons();
    }

    public void AddNewRandomWeapon()
    {
        GameObject prefab = WeaponFactory.Instance.GetRandomWeapon();
        GameObject instance = Instantiate(prefab);
        _weaponController.AttachWeapon(instance.GetComponent<Weapon>());
    }

    public void RemoveActiveWeapon()
    {
        _weaponController.DestroyActiveWeapon();
        Debug.Log("Removed Active Weapon");
    }

    private void OnWeaponChangedEvent(WeaponChangedEvent evt)
    {
        Debug.Log("Player " + evt.PlayerIndex + ": Weapon changed to " + evt.Weapon.Name);
    }

    private void OnWeaponFiredEvent(WeaponFiredEvent evt)
    {
        Debug.Log("Player " + evt.PlayerIndex + ": Weapon Fired" + evt.Weapon.Name);
    }
}
