using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MGprojectile : Poolable {

    public override void OnSpawn()
    {
        GetComponent<Rigidbody>().AddForce(transform.forward * 5000);
    }
    public override void OnDeSpawn()
    {
        base.OnDeSpawn();
    }
}
