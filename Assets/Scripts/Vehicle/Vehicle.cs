using UnityEngine;


[RequireComponent(typeof(VehicleMovement))]
public class Vehicle : MonoBehaviour
{
    public EController _controller;
    public SkinnedMeshRenderer[] Meshes;
    public Material[] Materials;

    private VehicleMovement _movement;

    private void Start()
    {
        _movement = GetComponent<VehicleMovement>();

        //HACK: Avoids changing the prefab because this reference keeps getting borked
        ItemController  _itemController = GetComponent<ItemController>();
        WeaponController  _weaponController = GetComponent<WeaponController>();
        _itemController.AttachWeaponController(_weaponController);
    }

    private void Update()
    {
        _movement.SetMovementDirection(new Vector2(PlayerInput.Instance.GetAxis(_controller, EControllerAxis.LeftHorizontal), PlayerInput.Instance.GetAxis(_controller, EControllerAxis.LeftVertical)));
        _movement.SetSteeringDirection(new Vector2(PlayerInput.Instance.GetAxis(_controller, EControllerAxis.RightHorizontal), PlayerInput.Instance.GetAxis(_controller, EControllerAxis.RightVertical)));
        float t = PlayerInput.Instance.GetAxis(_controller, EControllerAxis.LeftTrigger);
        _movement.SetThrottle(t);
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

    public void SetColor(EController controller)
    {
        foreach (SkinnedMeshRenderer mesh in Meshes)
        {
            mesh.material = Materials[(int)controller];
        }
    }

	private void OnTriggerEnter(Collider other)
	{
        if (other.gameObject.name == "KillVolume")
        {
            Publisher.Raise(new KillVolumeHitEvent(_controller, this));
        }
	}
}
