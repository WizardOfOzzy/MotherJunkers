using UnityEngine;
using System.Diagnostics;

public enum PickupType { weapon, boost, powerup };

public class Pickup : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Delete) || _timer.ElapsedMilliseconds > _duration)
        {
            Cleanup();
        }

        transform.Rotate(Vector3.up, _spinRate * Time.deltaTime);
    }

    public void Init(GameObject prefab, float setDuration,Location usedLocation)
    {
        _pickupType = prefab;
        _duration = setDuration;
        _usedLocation = usedLocation;
        
        prefab.transform.SetParent(transform);
        prefab.transform.localPosition = Vector3.zero;

        _timer = new Stopwatch();
        _timer.Start();
    }

    public void Cleanup()
    {
        UnityEngine.Debug.Log(_pickupType + " - " + _duration);
        SpawnManager.FreeLocation(_usedLocation);
        Destroy(gameObject);
    }

    public GameObject GetItem()
    {
        return transform.GetChild(0).gameObject;
    }

    private GameObject _pickupType;
    private float _duration;
    private Location _usedLocation;
    private Stopwatch _timer = new Stopwatch();
    private const float _spinRate = 60f;
}
