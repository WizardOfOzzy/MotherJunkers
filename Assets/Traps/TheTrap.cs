using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TheTrap : MonoBehaviour {

    Animation OptionalAnimationClip;

    public bool bDebugging = false;

    public float ImpulseAmount = 0.0f;
    public float DamageAmount = 0.0f;

    Collider DamageCollider;

    AudioSource audioSource;


    // Use this for initialization
    void Start () {

        OptionalAnimationClip = GetComponent<Animation>();
        DamageCollider = GetComponent<Collider>();
        audioSource = GetComponent<AudioSource>();

    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void ActivateTrap()
    {
        if (OptionalAnimationClip != null)
            OptionalAnimationClip.Play();
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
