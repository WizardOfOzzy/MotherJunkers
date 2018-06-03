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
        Hover,
        ThrottleBased
    }

    [SerializeField]
    ESteeringType SteeringType = ESteeringType.Hover;
    float _throttle = 0;
    // Use this for initialization
    void Start()
    {
        _RigidBody = GetComponent<Rigidbody>();
        _Camera = GameObject.FindObjectOfType<Camera>();
        SetSteeringType(SteeringType);
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
                PerformTankSteeringUpdate();
                break;
            case ESteeringType.ThrottleBased:
                PerformThrottleSteeringUpdate();
                break;
            default: 
            {
                PerformTankSteeringUpdate();
                break;
            }
        }
    }
    public void SetThrottle(float value )
    {
        _throttle=value;
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
                _TurningForce = 4f;
                break;
            case ESteeringType.ThrottleBased:
                _TurningForce = 4f;
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
        //ApplyTurningForce(transform.up * _SteeringDirection.x, _TurningForce);

        if(_SteeringDirection.magnitude > 0.05)
        {
            Vector3 camSpace = -_SteeringDirection.x * _Camera.transform.forward + _SteeringDirection.y * _Camera.transform.right;
            Vector3 axisDir = camSpace + transform.position;
            axisDir.y = transform.position.y;
            //_RigidBody.transform.LookAt(axisDir, _RigidBody.transform.up);

            //calculate the rotation needed 
            Quaternion neededRotation = Quaternion.LookRotation(axisDir - _RigidBody.transform.position);

            //use spherical interpollation over time 
            Quaternion interpolatedRotation = Quaternion.Slerp(_RigidBody.transform.rotation, neededRotation, Time.deltaTime * _TurningForce);
            _RigidBody.transform.rotation = interpolatedRotation;
        }

    }

    void PerformThrottleSteeringUpdate()
    {   
        if (_SteeringDirection.magnitude > 0.05)
        {
            Vector3 camSpace = -_SteeringDirection.x * _Camera.transform.forward + _SteeringDirection.y * _Camera.transform.right;
            Vector3 axisDir = camSpace + transform.position;
            axisDir.y = transform.position.y;
            //_RigidBody.transform.LookAt(axisDir, _RigidBody.transform.up);

            //calculate the rotation needed 
            Quaternion neededRotation = Quaternion.LookRotation(axisDir - _RigidBody.transform.position);

            //use spherical interpollation over time 
            Quaternion interpolatedRotation = Quaternion.Slerp(_RigidBody.transform.rotation, neededRotation, Time.deltaTime * _TurningForce);
            _RigidBody.transform.rotation = interpolatedRotation;
        }

        float AppliedBoost = _BoostOn ? _BoostMultiplier : 1.0f;

        ApplyForce(-transform.right, _ForceMultiplier*_throttle);

        // Truncate velocity
        if (_RigidBody.velocity.magnitude > _MaxVelocity * AppliedBoost)
        {
            _RigidBody.velocity = _RigidBody.velocity.normalized * _MaxVelocity * AppliedBoost;
        }
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
