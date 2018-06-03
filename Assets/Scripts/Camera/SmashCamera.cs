using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmashCamera : MonoBehaviour {

    VehicleMovement[] Vehicles;

    Camera MyCamera;

    Vector3 v3LookAtPosition;

	// Use this for initialization
	void Start () {

        Vehicles = FindObjectsOfType<VehicleMovement>();
        MyCamera = GetComponent<Camera>();

    }
	
	// Update is called once per frame
	void Update () {
        v3LookAtPosition = new Vector3(0, 0, 0);

        int numberofVehicles = 0;

        foreach (VehicleMovement Vhc in Vehicles)
        {
            numberofVehicles++;
            v3LookAtPosition += Vhc.transform.position;
        }

        v3LookAtPosition /= numberofVehicles;

        MyCamera.transform.LookAt(v3LookAtPosition);


    }
}
