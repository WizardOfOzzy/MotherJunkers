using System.Collections;
using UnityEngine;

public class RocketProjectile : MonoBehaviour
{
    private Rigidbody _rBody;

    // Use this for initialization
    private void Start()
    {
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
        if (other.GetComponent<VehicleHealth>())
        {
            VehicleHealth health = other.GetComponent<VehicleHealth>();

            health.TakeDamage(10);
            other.GetComponent<Rigidbody>().AddForce(transform.forward * (2000 + (health.Health * 20)));
        }

        Destroy(gameObject);
    }
}
