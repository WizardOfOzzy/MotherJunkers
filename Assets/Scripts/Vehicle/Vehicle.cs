using UnityEngine;

namespace MotherJunkers
{
    [RequireComponent(typeof(VehicleMovement))]
    public class Vehicle : MonoBehaviour
    {
        
        public EController _controller;

        VehicleMovement _movement;

        void Start()
        {
            _movement = GetComponent<VehicleMovement>();
        }

        private void Update()
        {
            _movement.SetMovementDirection(new Vector2(PlayerInput.Instance.GetAxis(_controller, EControllerAxis.LeftHorizontal), PlayerInput.Instance.GetAxis(_controller, EControllerAxis.LeftVertical)));
        }
    }
}