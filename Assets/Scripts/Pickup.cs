using UnityEngine;
using UnityEngine.Events;
public enum PickupType { weapon, boost, powerup };

public class Pickup : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Delete))
        {
            Cleanup();
        }
    }

    public void Init(PickupType ptype, float setDuration,Location usedLocation)
    {
        _pickupType = ptype;
        _duration = setDuration;
        _usedLocation = usedLocation;
    }

    public void Cleanup()
    {
        Debug.Log(_pickupType + " - " + _duration);
        SpawnManager.FreeLocation(_usedLocation);
        Destroy(gameObject);
    }

    public GameObject GetItem()
    {
        return transform.GetChild(0).gameObject;
    }

    private PickupType _pickupType;
    private float _duration;
    private Location _usedLocation;
}
