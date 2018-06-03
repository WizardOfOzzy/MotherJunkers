using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VehicleMovement : MonoBehaviour
{

    Rigidbody _RigidBody;
    Camera _Camera;

    [SerializeField]
    float _ForceMultiplier = 7500.0F;

    [SerializeField]
    float _MaxVelocity = 100.0F;

    [SerializeField]
    float _TurningForce = 80.0F;

    [SerializeField]
    float _MaxAngularVelocity = 200.0F;

    Vector2 _InputDirection;
    Vector2 _SteeringDirection;

    bool _BoostOn = false;

    [SerializeField]
    float _BoostMultiplier = 1.0f;

    public enum ESteeringType
    {
        Tank,
        Hover
    }

    [SerializeField]
    ESteeringType SteeringType = ESteeringType.Hover;
    
    // Use this for initialization
    void Start()
    {
        _RigidBody = GetComponent<Rigidbody>();
        _Camera = GameObject.FindObjectOfType<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        switch(SteeringType)
        {
            case ESteeringType.Hover: 
            {
                PerformHoverSteeringUpdate();
                break;
            }
            case ESteeringType.Tank: 
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

    public void SetSteeringDirection(Vector2 dir)
    {
        _SteeringDirection = dir;

        //Debug.Log("Direction : " + dir);
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

    public void SetSteeringType(ESteeringType newType)
    {
        switch(newType)
        {
            case ESteeringType.Tank:
                // Sets default tank values
                _ForceMultiplier = 5500.0f;
                _MaxVelocity = 10.0f;
                _TurningForce = 80.0f;
                _MaxAngularVelocity = 200.0f;
                _BoostMultiplier = 2.0f;
                break;
            case ESteeringType.Hover:
                break;
            default:
                break;
        }
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

    void PerformHoverSteeringUpdate()
    {
        float AppliedBoost = _BoostOn ? _BoostMultiplier : 1.0f;

        ApplyForce(_Camera.transform.right * _InputDirection.x, _ForceMultiplier);
        ApplyForce(_Camera.transform.up * _InputDirection.y, _ForceMultiplier);

        // Truncate velocity
        if (_RigidBody.velocity.magnitude > _MaxVelocity * AppliedBoost)
        {
            _RigidBody.velocity = _RigidBody.velocity.normalized * _MaxVelocity * AppliedBoost;
        }

        // Orientation
        ApplyTurningForce(transform.up * _SteeringDirection.x, _TurningForce);

        //Vector3 MyLookAt = (_Camera.transform.right * _SteeringDirection.x) + (_Camera.transform.up * _SteeringDirection.y);
        //transform.LookAt(MyLookAt);

        //Vector3 _camRel = _SteeringDirection.y * _Camera.transform.forward + _SteeringDirection.x * _Camera.transform.right;
        //Vector3 _axisDir = _camRel + transform.position;
        //Vector3 _LookAtTarget = new Vector3(_axisDir.x, transform.position.y, _axisDir.z);
        //this.transform.LookAt(_LookAtTarget, transform.up);

        //transform.Rotat
    }

    void Turn()
    {
        
    }

    //public void Rotate(float rotateLeftRight, float rotateUpDown, bool isPlayer)
    //{
    //    useUpdate = false;

    //    //Unsure of how much below code changes outcome.
    //    float sensitivity = 0;
    //    if (isPlayer)
    //    {
    //        sensitivity = .5f;
    //    }
    //    else
    //    {
    //        sensitivity = .25f;
    //    }

    //    //Get Main camera in Use.
    //    Camera cam = Camera.mainCamera;
    //    //Gets the world vector space for cameras up vector 
    //    Vector3 relativeUp = cam.transform.TransformDirection(Vector3.up);
    //    //Gets world vector for space cameras right vector
    //    Vector3 relativeRight = cam.transform.TransformDirection(Vector3.right);

    //    //Turns relativeUp vector from world to objects local space
    //    Vector3 objectRelativeUp = transform.InverseTransformDirection(relativeUp);
    //    //Turns relativeRight vector from world to object local space
    //    Vector3 objectRelaviveRight = transform.InverseTransformDirection(relativeRight);

    //    rotateBy = Quaternion.AngleAxis(rotateLeftRight / gameObject.transform.localScale.x * sensitivity, objectRelativeUp)
    //        * Quaternion.AngleAxis(-rotateUpDown / gameObject.transform.localScale.x * sensitivity, objectRelaviveRight);

    //    newDeltaObtained = true;

    //}
}
