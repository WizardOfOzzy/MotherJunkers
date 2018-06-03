using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VehicleMovement : MonoBehaviour
{

    Rigidbody _RigidBody;

    [SerializeField]
    float _ForceMultiplier = 7500.0F;

    [SerializeField]
    float _MaxVelocity = 100.0F;

    [SerializeField]
    float _TurningForce = 80.0F;

    [SerializeField]
    float _MaxAngularVelocity = 200.0F;

    Vector2 _InputDirection;

    // Use this for initialization
    void Start()
    {
        _RigidBody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        // Apply steering
        ApplyTurningForce(transform.up * _InputDirection.x, _TurningForce);

        // Apply forward movement
        ApplyForce(transform.right * -_InputDirection.y, _ForceMultiplier);

        // Truncate velocity
        if (_RigidBody.velocity.magnitude > _MaxVelocity)
        {
            _RigidBody.velocity = _RigidBody.velocity.normalized * _MaxVelocity;
        }

        if(_RigidBody.angularVelocity.magnitude > _MaxAngularVelocity)
        {
            _RigidBody.angularVelocity = _RigidBody.angularVelocity.normalized * _MaxVelocity;
        }
    }

    public void SetMovementDirection(Vector2 dir)
    {
        _InputDirection = dir;
    }

    void ApplyForce(Vector3 Direction, float force)
    {
        _RigidBody.AddForce(Direction * force * Time.deltaTime);
    }

    void ApplyTurningForce(Vector3 Direction, float force)
    {
        _RigidBody.AddTorque(Direction * force * Time.deltaTime);
    }
}
