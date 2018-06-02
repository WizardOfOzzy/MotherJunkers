using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VehicleMovement : MonoBehaviour
{

    Rigidbody _RigidBody;

    [SerializeField]
    float _ForceMultiplier = 10000.0F;

    [SerializeField]
    float _MaxVelocity = 100.0F;

    [SerializeField]
    float _TurningForce = 100.0F;

    [SerializeField]
    float _MaxAngularVelocity = 100.0F;

    // Use this for initialization
    void Start()
    {
        _RigidBody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        float x;
        float y;

        CollectInput(out x, out y);

        Debug.Log("x : " + x);
        Debug.Log("y : " + y);

        // Apply steering
        ApplyTurningForce(transform.up * x, _TurningForce);

        // Apply forward movement
        ApplyForce(transform.forward * y, _ForceMultiplier);

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

    void CollectInput(out float x, out float y)
    {
        x = Input.GetAxis("Horizontal");
        y = Input.GetAxis("Vertical");
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
