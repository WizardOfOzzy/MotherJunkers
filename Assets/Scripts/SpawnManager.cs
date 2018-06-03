using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
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

    public int spawnCount = 20;
    public float interval = 10000f;
    bool ready = false;
    
    void Start()
    {
        InvokeRepeating("SpawnRandom", 1f, 10f); 
    }

    void SpawnRandom() {
       Spawn(PickupType.weapon);
    }

	// Update is called once per frame
	void Update () {
		if (Input.GetKeyUp(KeyCode.Space))
        {
            SpawnRandom();
        }
    }
    #endregion

    #region Public Declarations
    public GameObject WeaponPrefab;
    public GameObject BoostPrefab;
    public GameObject PowerupPrefab;
    public static List<Location> locations = new List<Location>();
    public GameObject SpawnLocations;
    public bool isBoost = true;
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

            Pickup pickup = newPickup.GetComponent<Pickup>();
            pickup.Init(someType, 8000f, foundLocation);
            
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

        List<Location> someLocations = locations.FindAll(s => s.available);

        if (someLocations.Count > 0)
        {
            System.Random rnd = new System.Random();
            int r = rnd.Next(someLocations.Count);
            availableLocation = locations[r];
            availableLocation.available = false;

            locations.RemoveAt(r);
            locations.Add(availableLocation);
        }
        else
        {
            UnityEngine.Debug.Log("No spawn positions found!!");
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
