using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VehicleMovement : MonoBehaviour
{

    Rigidbody _RigidBody;
    Camera _Camera;

    	/** Mass of the vehicle */
	[SerializeField]
	public float _Mass;

	/** Velocity of this object */
	Vector3 _Velocity;

	/** Maximum speed at which we can travel */
	[SerializeField]
	public float _MaxSpeed;

	/** Maximum force which can be applied to this vehicle */
	[SerializeField]
	public float _MaxForce;

	/** Maximum rate (radians per second) this vehicle can turn */
	[SerializeField]
	public float _MaxTurnRate;

	/** Amount of braking to apply to the desired velocity */
	[SerializeField]
	public float _Brake;

	/** Amount of boost to apply to the vehicle */
	[SerializeField]
	public float _BoostMultiplier;

	/** Relative target to which we are attempting to move (Used for debugging) */
	Vector3 _TargetDestination;

    Vector2 _inputDirection;

	/** Maximum distance for the target destination */
	[SerializeField]
	public float _MaxTargetDistance;

    [SerializeField]
    float _ForceMultiplier;

    [SerializeField]
    float _MaxVelocity;

    // Use this for initialization
    void Start()
    {
        _RigidBody = GetComponent<Rigidbody>();
        _Camera = GameObject.FindObjectOfType<Camera>();
    }

     public void SetMovementDirection(Vector2 direction)
    {
        _inputDirection = direction;
    }

    Vector3 CalculateForce()
	{
		Vector3 DesiredVelocity = (_TargetDestination - transform.position);
		DesiredVelocity.Normalize ();
		DesiredVelocity *= _MaxSpeed;

		return (DesiredVelocity - _Velocity);
	}

    private void FixedUpdate()
    {    
        // Apply horizontal force
        ApplyForce(_Camera.transform.right * _inputDirection.x, _ForceMultiplier);

        // Apply vertical force
        ApplyForce(_Camera.transform.up * transform.position.y, _ForceMultiplier);

        // Truncate velocity
        if (_RigidBody.velocity.magnitude > _MaxVelocity)
        {
            _RigidBody.velocity = _RigidBody.velocity.normalized * _MaxVelocity;
        }
    }

    void ApplyForce(Vector3 Direction, float force)
    {
        _RigidBody.AddForce(Direction * force * Time.deltaTime);
    }
}
