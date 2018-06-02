using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class TestVehicle : MonoBehaviour
{
    Rigidbody MyRigidBody;
    Animator AnimationController;

    Camera MainCamera;


    [SerializeField]
    float SpeedMultiplier = 100000.0f;

    // Use this for initialization
    void Start()
    {
        MyRigidBody = GetComponent<Rigidbody>();
        AnimationController = GetComponent<Animator>();

        MainCamera = GameObject.FindObjectOfType<Camera>();
    }

    // Update is called once per frame
    void Update()
    {

        // Hack, if the game has not started, bail
        //if (GameManager.Instance == null || !GameManager.Instance.gameInProgress)
        //{
        //    return;
        //}
        //Move(MainCamera.transform.up, SpeedMultiplier * Input.GetAxis(baseControllable.InputHandles.VerticalAxis));
        //Move(MainCamera.transform.right, SpeedMultiplier * Input.GetAxis(baseControllable.InputHandles.HorizontalAxis));


        if (Input.GetKey(KeyCode.W))
        {
            Move(MainCamera.transform.up, SpeedMultiplier);
        }
        if (Input.GetKey(KeyCode.S))
        {
            Move(-MainCamera.transform.up, SpeedMultiplier);
        }

        if (Input.GetKey(KeyCode.A))
        {
            Move(-MainCamera.transform.right, SpeedMultiplier);
        }

        if (Input.GetKey(KeyCode.D))
        {
            Move(MainCamera.transform.right, SpeedMultiplier);
        }


        if (MyRigidBody.velocity.magnitude > 1.0f)
            transform.forward = MyRigidBody.velocity.normalized;

    }

    private void OnDestroy()
    {
        Debug.Log("Destroyed" + this);
    }


    void Move(Vector3 Direction, float force)
    {
        //MyRigidBody.AddRelativeTorque(new Vector3(0.0f,10.0f,0.0f));
        MyRigidBody.AddForce(Direction * force * Time.deltaTime);

    }
}
