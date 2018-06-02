using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemController : MonoBehaviour 
{
	private void OnTriggerEnter(Collider other)
	{
        Item item = other.GetComponentInChildren<Item>();

        if (item != null)
        {
            AddPickup(item);
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
        else if (item is SpecialItem)
        {
            AddSpecial(item as SpecialItem);
        }
    }

    private void AddWeapon(Weapon weapon)
    {
        // TODO - add weapon to WeaponController, re-parent Weapon GameObject
    }

    private void AddBoost(BoostItem item)
    {
        // TODO - add boost to BoostController, destroy Boost GameObject
    }

    private void AddSpecial(SpecialItem item)
    {
        // TODO - do whatever with our QuadDamage, destroy Special GameObject
    }
}
