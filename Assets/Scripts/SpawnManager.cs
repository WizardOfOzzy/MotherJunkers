using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour {
    private readonly List<GameObject> pickups = new List<GameObject>();
   
    #region Mono

    private void Awake()
    {
        int locationCount = SpawnLocations.transform.childCount;
        for (int i = 0; i < locationCount; i++)
        {
            GameObject foundObject = SpawnLocations.transform.GetChild(i).gameObject;
            locations.Add(new Location(foundObject));
            foundObject.SetActive(false);
        }
    }

    private void Start()
    {
        InvokeRepeating("SpawnRandom", 1f, 10f); 
    }

    private void SpawnRandom() {
       Spawn(PickupType.weapon);
    }

	// Update is called once per frame
    private void Update () {
		if (Input.GetKeyUp(KeyCode.K))
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
    #endregion

    #region Public Methods
    public void Spawn(PickupType someType)
    {
        GameObject selectedPrefab = null;
        Location foundLocation = GetAvailablePosition();
        
        if (!foundLocation.Equals(default(Location)))
        {
            switch (someType)
            {
                case PickupType.weapon:
                    selectedPrefab = WeaponPrefab;
                    break;
                case PickupType.boost:
                    selectedPrefab = BoostPrefab;
                    break;
                case PickupType.powerup:
                    selectedPrefab = PowerupPrefab;
                    break;
            }

            GameObject newPickup = Instantiate(selectedPrefab);
            newPickup.transform.SetParent(transform);

            newPickup.transform.localPosition = foundLocation.actualObj.transform.localPosition;

            Pickup pickup = newPickup.GetComponent<Pickup>();
            pickup.Init(someType, 8000f, foundLocation);

            pickups.Add(newPickup);
        }
    }

    public static void FreeLocation(Location loc)
    {
        int someLocation = locations.FindLastIndex(s => s.Equals(loc));
        if (someLocation != -1)
        {
            Location locationToFree = locations[someLocation];
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
