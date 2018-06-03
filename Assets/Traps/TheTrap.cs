using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TheTrap : MonoBehaviour {

    Animation OptionalAnimationClip;

    Rigidbody MyRigidBody;

    public bool bDebugging = false;

    public Vector3 ImpulseDirection;

    public bool bUseToColliderForward = false;

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

        else if (MyRigidBody)
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

            if (OptionalAnimationClip != null)
            {
                Rigidbody RB = contact.otherCollider.GetComponent<Rigidbody>();

                Vector3 ToCollidery = contact.otherCollider.transform.position - transform.position;

                Vector3 impulsenomral = new Vector3(ToCollidery.x, 0, ToCollidery.z);

                impulsenomral.Normalize();

                if(bUseToColliderForward)
                    transform.parent.forward = impulsenomral;

                RB.AddForce(impulsenomral * ImpulseAmount);
            }
        }
            
    }

}
