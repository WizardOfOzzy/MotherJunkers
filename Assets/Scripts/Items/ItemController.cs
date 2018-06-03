using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemController : MonoBehaviour 

{
    [SerializeField]
    private WeaponController _weaponController;

    private void OnTriggerEnter(Collider other)
    {
        
        Pickup pickup = other.GetComponent<Pickup>();

        if (pickup != null)
        {
            
            Item item = pickup.GetItem().GetComponent<Item>();
            AddPickup(item);
            pickup.Cleanup();
        }
    }

	private void AddPickup(Item item)
    {
        // Embrace the jank
        if (item is Weapon)
        {
            AddWeapon(item as Weapon);
        }
        else if (item is BoostItem)
        {
            AddBoost(item as BoostItem);
        }
    }

    private void AddWeapon(Weapon weapon)
    {
        _weaponController.AttachWeapon(weapon);
    }

    private void AddBoost(BoostItem item)
    {
        // TODO - add boost to BoostController, destroy Boost GameObject
    }
}
