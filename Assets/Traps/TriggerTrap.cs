using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerTrap : MonoBehaviour {

    TheTrap [] Traps;
    //TheTrigger Trigger;


    // Use this for initialization
    void Start () {
        Traps = GetComponentsInChildren<TheTrap>();

	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void ActivateAllTraps()
    {
       foreach(TheTrap Trap in Traps)
        {
            Trap.ActivateTrap();
        }
            
    }
}
