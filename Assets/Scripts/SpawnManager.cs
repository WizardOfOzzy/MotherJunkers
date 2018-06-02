using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour {
    List<GameObject> pickups = new List<GameObject>();
   
    #region Mono

    void Awake()
    {
        int locationCount = SpawnLocations.transform.childCount;
        for (int i = 0; i < locationCount; i++)
        {
            GameObject foundObject = SpawnLocations.transform.GetChild(i).gameObject;
            locations.Add(new Location(foundObject));
            foundObject.SetActive(false);
        }
    }

    void Start()
    {
        Spawn(PickupType.weapon);
        Spawn(PickupType.boost);
        Spawn(PickupType.powerup);
        Spawn(PickupType.weapon);
        Spawn(PickupType.boost);
        Spawn(PickupType.powerup);
    }

	// Update is called once per frame
	void Update () {
		if (Input.GetKeyUp(KeyCode.Space))
        {
            Spawn(PickupType.weapon);
        }
    }
    #endregion

    #region Public Declarations
    public GameObject WeaponPrefab;
    public GameObject BoostPrefab;
    public GameObject PowerupPrefab;
    public static List<Location> locations = new List<Location>();
    public GameObject SpawnLocations;
    #endregion

    #region Public Methods
    public void Spawn(PickupType someType)
    {
        GameObject selectedPrefab = null;

        if (someType == PickupType.weapon)
        {
            selectedPrefab = WeaponPrefab;
        }
        else if (someType == PickupType.boost)
        {
            selectedPrefab = BoostPrefab;
        }
        else if (someType == PickupType.powerup)
        {
            selectedPrefab = PowerupPrefab;
        }

        GameObject newPickup = Instantiate(selectedPrefab);
        newPickup.transform.SetParent(transform);
        Location foundLocation = GetAvailablePosition();

        if (!foundLocation.Equals(default(Location)))
        {
            newPickup.transform.localPosition = foundLocation.actualObj.transform.localPosition;

            Pickup weapon = newPickup.GetComponent<Pickup>();
            weapon.Init(someType, 2000f, foundLocation);
            
            pickups.Add(newPickup);
        }
    }

    public static void FreeLocation(Location loc)
    {
        int someLocation = locations.FindLastIndex(s => s.Equals(loc));
        Location locationToFree;
        if (someLocation != -1)
        {
            locationToFree = locations[someLocation];
            locationToFree.available = true;

            locations.RemoveAt(someLocation);
            locations.Add(locationToFree);
        }
    }
    #endregion

    #region Private Methods

    private Location GetAvailablePosition()
    {
        Location availableLocation = default(Location);

        int someLocation = locations.FindLastIndex(s => s.available);

        if (someLocation != -1)
        {
            availableLocation = locations[someLocation];
            availableLocation.available = false;

            locations.RemoveAt(someLocation);
            locations.Add(availableLocation);
        }
        else
        {
            Debug.Log("No spawn positions found!!");
        }

        return availableLocation;
    }
    #endregion
}

public struct Location
{
    public GameObject actualObj;
    public bool available;

    public Location(GameObject obj)
    {
        actualObj = obj;
        available = true;
    }
}
