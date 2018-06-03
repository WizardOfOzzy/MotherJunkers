using UnityEngine;

namespace MotherJunkers
{
    [RequireComponent(typeof(VehicleMovement))]
    public class Vehicle : MonoBehaviour
    {
        
        public EController _controller;

        public Color _Color;

        VehicleMovement _movement;

        void Start()
        {
            _movement = GetComponent<VehicleMovement>();
        }

        private void Update()
        {
            _movement.SetMovementDirection(new Vector2(PlayerInput.Instance.GetAxis(_controller, EControllerAxis.LeftHorizontal), PlayerInput.Instance.GetAxis(_controller, EControllerAxis.LeftVertical)));

            // Check for boost
            if (PlayerInput.Instance.GetButtonDown(_controller, EControllerButton.RightBumper))
            {
                _movement.BoostOn();
            }
            if (PlayerInput.Instance.GetButtonUp(_controller, EControllerButton.RightBumper))
            {
                _movement.BoostOff();
            }

        }

        public void SetColor(Color pColor)
        {
            _Color = pColor;
            Material VehcMat = GetComponent<Material>();
            if (VehcMat != null)
                VehcMat.color = pColor;
        }
    }
}