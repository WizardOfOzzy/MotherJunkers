using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TheTrigger : MonoBehaviour {

    TriggerTrap MyTriggerTrap;

    public int AmountOfTriggers = 10000;

    bool bCanTrigger = true;

	// Use this for initialization
	void Start () {
        MyTriggerTrap = GetComponentInParent<TriggerTrap>();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerEnter(Collider other)
    {
        if (AmountOfTriggers <= 0 || !bCanTrigger)
            return;

        --AmountOfTriggers;

        bCanTrigger = false;

        MyTriggerTrap.ActivateAllTraps();
    }

    void OnTriggerExit(Collider other)
    {
        bCanTrigger = true;
    }


    }
