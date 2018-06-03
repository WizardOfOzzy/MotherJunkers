using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmashCamera : MonoBehaviour 
{
    MotherJunkers.Vehicle[] Vehicles;
    Camera MyCamera;
    Vector3 v3LookAtPosition;


	public void Init(MotherJunkers.Vehicle[] vehicles)
	{
        Vehicles = vehicles;
        MyCamera = GetComponent<Camera>();
	}

    // Update is called once per frame
    void Update()
    {
        if (Vehicles != null && MyCamera != null)
        {
            v3LookAtPosition = new Vector3(0, 0, 0);

            int numberofVehicles = 0;

            foreach (MotherJunkers.Vehicle Vhc in Vehicles)
            {
                if (Vhc != null)
                {
                    numberofVehicles++;
                    v3LookAtPosition += Vhc.transform.position;
                }
            }

            v3LookAtPosition /= numberofVehicles;
            MyCamera.transform.LookAt(v3LookAtPosition);
        }
    }
}
