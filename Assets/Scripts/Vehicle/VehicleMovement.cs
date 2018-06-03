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

    bool _BoostOn = false;

    [SerializeField]
    float _BoostMultiplier = 1.0f;

    public enum ESteeringType
    {
        Tank
    }

    [SerializeField]
    ESteeringType SteeringType;
    
    // Use this for initialization
    void Start()
    {
        _RigidBody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        switch(SteeringType)
        {
            default: 
            {
                PerformTankSteeringUpdate();
                break;
            }
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

    public void BoostOn()
    {
        _BoostOn = true;
    }
    public void BoostOff()
    {
        _BoostOn = false;
    }

    void PerformTankSteeringUpdate()
    {
        // Apply steering
        ApplyTurningForce(transform.up * _InputDirection.x, _TurningForce);

        // Apply forward movement
        float AppliedBoost = _BoostOn ? _BoostMultiplier : 1.0f;
        ApplyForce(transform.right * -_InputDirection.y, _ForceMultiplier * AppliedBoost);

        // Truncate velocity
        if (_RigidBody.velocity.magnitude > _MaxVelocity * AppliedBoost)
        {
            _RigidBody.velocity = _RigidBody.velocity.normalized * _MaxVelocity * AppliedBoost;
        }

        if (_RigidBody.angularVelocity.magnitude > _MaxAngularVelocity)
        {
            _RigidBody.angularVelocity = _RigidBody.angularVelocity.normalized * _MaxVelocity;
        }
    }
}
