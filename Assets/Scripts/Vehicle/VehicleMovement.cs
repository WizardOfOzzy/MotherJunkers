using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VehicleMovement : MonoBehaviour
{

    Rigidbody _RigidBody;
    Camera _Camera;

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

    // Update is called once per frame
    void Update()
    {
        float x;
        float y;

        CollectInput(out x, out y);

        //Debug.Log("x : " + x);
        //Debug.Log("y : " + y);

        // Apply horizontal force
        ApplyForce(_Camera.transform.right * x, _ForceMultiplier);

        // Apply vertical force
        ApplyForce(_Camera.transform.up * y, _ForceMultiplier);

        // Truncate velocity
        if (_RigidBody.velocity.magnitude > _MaxVelocity)
        {
            _RigidBody.velocity = _RigidBody.velocity.normalized * _MaxVelocity;
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
}
