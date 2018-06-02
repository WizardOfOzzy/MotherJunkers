using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapDamageCollider : MonoBehaviour {

    public bool bDebugging = false;

    public float ImpulseAmount = 0.0f;
    public float DamageAmount = 0.0f;

    public Collider DamageCollider;

    AudioSource audioSource;


    void Start () {

        DamageCollider = GetComponent<Collider>();
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update () {
		
	}

    void OnCollisionEnter(Collision collision)
    {
        
        foreach (ContactPoint contact in collision.contacts)
        {
            if (bDebugging)
                Debug.DrawRay(contact.point, contact.normal, Color.white);

            Rigidbody RB = contact.otherCollider.GetComponent<Rigidbody>();

            RB.AddForce(contact.normal * ImpulseAmount);
        }

        if (collision.relativeVelocity.magnitude > 2)
            audioSource.Play();
    }


}
