using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketProjectile : MonoBehaviour {
    Rigidbody _rBody;
    // Use this for initialization
    void Start () {
        _rBody = GetComponent<Rigidbody>();
        _rBody.velocity = Vector3.zero;
        _rBody.AddForce(transform.forward * 2500);
        StartCoroutine("DeSpawnIn", 1);
        GetComponent<VFX>().Play();
    }

    public IEnumerator DeSpawnIn(float time)
    {
        yield return new WaitForSeconds(time);
        Destroy(this.gameObject);
    }
    private void OnTriggerEnter(Collider other)
    {
        Destroy(this.gameObject);
    }

}
