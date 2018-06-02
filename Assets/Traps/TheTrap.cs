using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TheTrap : MonoBehaviour {

    Animation OptionalAnimationClip;

    Rigidbody MyRigidBody;

    public bool bDebugging = false;

    public Vector3 ImpulseDirection;

    public float ImpulseAmount = 0.0f;
    public float DamageAmount = 0.0f;

    Collider DamageCollider;

    AudioSource audioSource;


    // Use this for initialization
    void Start () {

        OptionalAnimationClip = GetComponent<Animation>();
        DamageCollider = GetComponent<Collider>();
        audioSource = GetComponent<AudioSource>();
        MyRigidBody = GetComponent<Rigidbody>();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void ActivateTrap()
    {
        if (OptionalAnimationClip != null)
            OptionalAnimationClip.Play();

        if (MyRigidBody)
            MyRigidBody.AddForce(ImpulseDirection * ImpulseAmount);

        if(audioSource)
            audioSource.Play();
    }


    void OnCollisionEnter(Collision collision)
    {

        foreach (ContactPoint contact in collision.contacts)
        {
            if (bDebugging)
                Debug.DrawRay(contact.point, contact.normal, Color.white);

            //Rigidbody RB = contact.otherCollider.GetComponent<Rigidbody>();
            //
            //RB.AddForce(contact.normal * ImpulseAmount);
        }
            
    }

}
