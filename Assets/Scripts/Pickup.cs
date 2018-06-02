using UnityEngine;

public enum PickupType { weapon, boost, powerup };

public class Pickup : MonoBehaviour
{
    public void Init(PickupType ptype, float setDuration)
    {
        _pickupType = ptype;
        _duration = setDuration;
    }

    public void Apply()
    {

    }

    private PickupType _pickupType;
    private float _duration;
    private Vector3 _spawnPosition;
    private bool _consumed;
}
