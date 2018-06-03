using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class VehicleHealth : MonoBehaviour {

    public float Health = 0.0f;

    // Use this for initialization
    void Start() {

    }

    // Update is called once per frame
    void Update() {

    }

    public void TakeDamage(float pDamage)
    {
        Health -= pDamage;

        DamageTakenEvent evt = new DamageTakenEvent((int)GetComponent<Vehicle>()._controller, Health);
        Publisher.Raise<DamageTakenEvent>(evt);

    }
}
