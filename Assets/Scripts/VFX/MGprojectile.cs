using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MGprojectile : Poolable {
    Rigidbody _rBody;
    public override void OnCreation()
    {
        base.OnCreation();
        _rBody = GetComponent<Rigidbody>();
    }
    public override void OnSpawn()
    {
        _rBody.velocity = Vector3.zero;
        _rBody.AddForce(transform.forward * 5000);
        StartCoroutine("DeSpawnIn",1);
    }
    public override void OnDeSpawn()
    {
        base.OnDeSpawn();
        _rBody.velocity = Vector3.zero;
        StopCoroutine("DeSpawnIn");
    }
    public IEnumerator DeSpawnIn(float time)
    {
        yield return new WaitForSeconds(time);
        MachineGunPool.Instance.ReturnProjectile(this);
    }
}
