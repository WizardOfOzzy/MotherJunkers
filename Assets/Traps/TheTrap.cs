using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TheTrap : MonoBehaviour {

    Animation OptionalAnimationClip;
	

	// Use this for initialization
	void Start () {

        OptionalAnimationClip = GetComponent<Animation>();
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void ActivateTrap()
    {
        if (OptionalAnimationClip != null)
            OptionalAnimationClip.Play();
    }
}
