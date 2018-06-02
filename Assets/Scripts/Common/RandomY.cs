using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomY : MonoBehaviour {
    

	// Use this for initialization
	void Start () {

        transform.Rotate(new Vector3(0,1,0),Random.Range(0, 360));
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
