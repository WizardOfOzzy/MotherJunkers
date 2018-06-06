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
	    Weapon weapon = item as Weapon;
	    if (weapon != null)
        {
            AddWeapon(weapon);
        }
        else
	    {
	        BoostItem boostItem = item as BoostItem;
	        if (boostItem != null)
	        {
	            AddBoost(boostItem);
	        }
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

    public void AttachWeaponController(WeaponController controller)
    {
        _weaponController = controller;
    }
}
