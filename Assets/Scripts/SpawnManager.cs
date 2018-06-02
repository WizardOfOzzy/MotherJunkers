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
		
	}
    #endregion

    #region Public Declarations
    public GameObject WeaponPrefab;
    public GameObject BoostPrefab;
    public GameObject PowerupPrefab;
    private List<Location> locations = new List<Location>();
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
        newPickup.transform.localPosition = GetAvailablePosition();


        Pickup weapon = newPickup.GetComponent<Pickup>();
        weapon.Init(someType, 0.5f);

        pickups.Add(newPickup);
    }

    private Vector3 GetAvailablePosition()
    {
        Vector3 availablePosition = Vector3.zero;

        int someLocation = locations.FindLastIndex(s => s.available);
        
        if (someLocation != -1)
        {
            Location foundLocation = locations[someLocation];
            availablePosition = foundLocation.actualObj.transform.localPosition;
            foundLocation.available = false;

            locations.RemoveAt(someLocation);
            locations.Add(foundLocation);
        }
        else
        {
            Debug.Log("No spawn positions found!!");
        }

        return availablePosition;
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
