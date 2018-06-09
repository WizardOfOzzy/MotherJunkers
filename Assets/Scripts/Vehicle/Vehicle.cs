using System.Collections;
using UnityEngine;


[RequireComponent(typeof(VehicleMovement))]
public class Vehicle : MonoBehaviour
{
    public EController _controller;
    public SkinnedMeshRenderer[] Meshes;
    public Material[] Materials;

    private VehicleMovement _movement;

    private Color[] _colors;

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
        Debug.Log("SetColor: " + controller);

        foreach (SkinnedMeshRenderer mesh in Meshes)
        {
            mesh.material = Materials[(int)controller - 1];
        }

        _colors = new Color[Meshes.Length];

        for (int i = 0; i < Meshes.Length; i++)
        {
            SkinnedMeshRenderer mesh = Meshes[i];
            _colors[i] = mesh.material.GetColor("_Color");
        }
    }

	private void OnTriggerEnter(Component other)
	{
        if (other.gameObject.name == "KillVolume")
        {
            Publisher.Raise(new KillVolumeHitEvent(_controller, this));
        }
	}

    public void FlashPlayer()
    {
        StartCoroutine(IFlashPlayer());
    }

    private IEnumerator IFlashPlayer()
    {
        for (int i = 0; i < 10; i++)
        {
            yield return new WaitForSeconds(0.1f);
            for (int j = 0; j < Meshes.Length; j++)
            {
                SkinnedMeshRenderer mesh = Meshes[j];
                Color gray = Color.gray;
                gray.a = 0.1f;
                mesh.material.SetColor("_Color", gray);
                yield return new WaitForSeconds(0.1f);
                mesh.material.SetColor("_Color", _colors[j]);
            }
        }
    }
}
