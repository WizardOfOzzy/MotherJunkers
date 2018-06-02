using UnityEngine;

namespace MotherJunkers
{
    [RequireComponent(typeof(VehicleMovement))]
    public class Vehicle : MonoBehaviour
    {
        [SerializeField]
        EController _controller;

        VehicleMovement _movement;

        private void Start()
        {
            _movement = GetComponent<VehicleMovement>();
        }
    }
}