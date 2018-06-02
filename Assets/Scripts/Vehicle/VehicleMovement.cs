using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VehicleMovement : MonoBehaviour {

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

	/** Maximum distance for the target destination */
	[SerializeField]
	public float _MaxTargetDistance;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () 
	{

		// Track the current position
		Vector3 OldPos = gameObject.transform.position;

		// Get input
		Vector3 targetDestination = new Vector3(Input.GetAxis("Horizontal") * _MaxTargetDistance,  0.0F, Input.GetAxis ("Vertical") * _MaxTargetDistance);
		_TargetDestination = OldPos + targetDestination;
		_TargetDestination.y = 0.0F;

		UpdatePosition ();
	}

	Vector3 CalculateForce()
	{
		Vector3 DesiredVelocity = (_TargetDestination - transform.position);
		DesiredVelocity.Normalize ();
		DesiredVelocity *= _MaxSpeed;

		return (DesiredVelocity - _Velocity);
	}

	/**
	 *  Position update
	 */
	void UpdatePosition()
	{
		// Calculate the desired steering force
		Vector3 SteeringForce = CalculateForce ();

		// Acceleration = force / mass
		Vector3 Acceleration = SteeringForce / _Mass;

		// Update velocity
		_Velocity += Acceleration * Time.deltaTime;

		// Truncate the velocity if it exceeds our maximum amount of force
		if (_Velocity.sqrMagnitude > (_MaxForce * _MaxForce)) 
		{
			_Velocity = _Velocity.normalized * _MaxForce;
		}

		// Update the position
		gameObject.transform.position += _Velocity * Time.deltaTime;
	}

	/**
	 *  Orientation update
	 */
	void UpdateOrientation()
	{
		// Stubbed in to not rotate for now
		Quaternion OldOrientation = gameObject.transform.rotation;
		Quaternion NewOrientaiton = OldOrientation;	
	}
}
